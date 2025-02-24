<template>
    <div>
        <div role="tablist" class="tabs w-full" :class="[`tabs-${variant}`, `tabs-${size}`]">
            <button
                v-for="tab in tabs"
                :key="tab.id"
                role="tab"
                class="tab px-2"
                :class="{ 'tab-active': activeTab === tab.id }"
                :style="{ 'max-width': tabMaxWidth }"
                @click="selectTab(tab.id)"
            >
                <span
                    class="inline-block overflow-hidden whitespace-nowrap text-ellipsis"
                    :style="`width: calc(100% - ${(Number(showEditButton) + Number(showCloseButton)) * 1.5}rem)`"
                >
                    {{ tab.label }}
                </span>
                <span
                    v-if="showEditButton"
                    class="btn btn-xs btn-square btn-ghost mdi mdi-pencil"
                    @click.stop="emits('edit', tab.id)"
                ></span>
                <span
                    v-if="showCloseButton"
                    class="btn btn-xs btn-square btn-ghost mdi mdi-close"
                    @click.stop="onCloseTab(tab.id)"
                ></span>
            </button>
        </div>

        <Teleport :to="teleportTarget" :disabled="!teleportTarget" defer>
            <div class="h-full relative mt-4">
                <template v-for="tab in tabs" :key="tab.id">
                    <div v-show="activeTab === tab.id" role="tabpanel" class="h-full">
                        <slot v-if="!useDefaultSlot" :name="tab.id"></slot>
                        <slot v-else :props="{ tab }"></slot>
                    </div>
                </template>
            </div>
        </Teleport>
    </div>
</template>

<script setup lang="ts" generic="T extends Tab">
import { ref, watch } from 'vue';

export type Tab = {
    id: string;
    label: string;
};

type TabVariant = 'boxed' | 'bordered' | 'lifted' | '';
type TabSize = 'xs' | 'sm' | 'md' | 'lg';

const props = withDefaults(
    defineProps<{
        tabs: T[];
        defaultTab?: string;
        size?: TabSize;
        variant?: TabVariant;
        tabMaxWidth?: string;
        teleportTarget?: string | HTMLElement;
        showEditButton?: boolean;
        showCloseButton?: boolean;
        useDefaultSlot?: boolean;
    }>(),
    {
        size: 'md',
        variant: 'bordered',
        defaultTab: undefined,
        tabMaxWidth: '100%',
        teleportTarget: undefined,
        showEditButton: false,
        showCloseButton: false,
        useDefaultSlot: false,
    }
);

const emits = defineEmits<{
    close: [id: string];
    edit: [id: string];
    'tab-selected': [id: string | undefined];
}>();

const activeTab = ref(props.defaultTab || props.tabs.at(0)?.id);

watch(
    () => props.tabs,
    (current, previous) => {
        if (current.length === 0) {
            selectTab(undefined);
            return;
        }

        if (!activeTab.value) {
            selectTab(current[0].id);
            return;
        }

        if (current.length > previous.length) {
            selectTab(current[current.length - 1].id);
            return;
        }
    }
);

const selectTab = (tabId: string | undefined) => {
    activeTab.value = tabId;
    emits('tab-selected', tabId);
};

const onCloseActiveTab = () => {
    const closedTabIndex = props.tabs.findIndex((tab) => tab.id === activeTab.value);
    const leftTabIndex = closedTabIndex - 1;
    selectTab(props.tabs[leftTabIndex]?.id);
};

const onCloseTab = (tabId: string) => {
    if (tabId === activeTab.value) {
        onCloseActiveTab();
    }

    emits('close', tabId);
};
</script>
