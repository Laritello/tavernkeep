<template>
    <div>
        <div role="tablist" class="tabs w-full" :class="[`tabs-${variant}`, `tabs-${size}`]">
            <button
                v-for="tab in tabs"
                :key="tab.id"
                role="tab"
                class="tab"
                :class="{ 'tab-active': activeTab === tab.id }"
                @click="selectTab(tab.id)"
            >
                <span class="flex flex-row w-full">
                    <span class="grow">{{ tab.label }}</span>
                    <span
                        v-if="showEditButton"
                        class="btn btn-xs btn-square btn-ghost mdi mdi-pencil"
                        @click="emits('edit', tab.id)"
                    ></span>
                    <span
                        v-if="showCloseButton"
                        class="btn btn-xs btn-square btn-ghost mdi mdi-close"
                        @click="emits('close', tab.id)"
                    ></span>
                </span>
            </button>
        </div>

        <Teleport :to="teleportTarget" :disabled="!teleportTarget">
            <div class="h-full relative mt-4">
                <template v-for="tab in tabs" :key="tab.id">
                    <div v-show="activeTab === tab.id" role="tabpanel" class="h-full">
                        <slot :name="tab.id"></slot>
                    </div>
                </template>
            </div>
        </Teleport>
    </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';

type Tab = {
    id: string;
    label: string;
};

type TabVariant = 'boxed' | 'bordered' | 'lifted' | '';
type TabSize = 'xs' | 'sm' | 'md' | 'lg';

const props = withDefaults(
    defineProps<{
        tabs: Tab[];
        defaultTab?: string;
        size?: TabSize;
        variant?: TabVariant;
        teleportTarget?: string | HTMLElement;
        showEditButton?: boolean;
        showCloseButton?: boolean;
    }>(),
    {
        size: 'md',
        variant: 'bordered',
        defaultTab: '',
        teleportTarget: undefined,
        showEditButton: false,
        showCloseButton: false,
    }
);

const emits = defineEmits<{
    close: [id: string];
    edit: [id: string];
}>();

const activeTab = ref(props.defaultTab || props.tabs[0]?.id || '');

watch(
    () => props.tabs,
    (newTabs) => {
        if (!newTabs.some((tab) => tab.id === activeTab.value)) {
            activeTab.value = newTabs[0]?.id || '';
        }
    }
);

const selectTab = (tabId: string) => {
    activeTab.value = tabId;
};
</script>
