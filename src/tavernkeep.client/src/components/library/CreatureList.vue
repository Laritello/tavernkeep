<script setup lang="ts">
import { useVirtualList } from '@vueuse/core';
import { computed } from 'vue';

import type { CreatureShort } from '@/contracts/creatures/CreatureShort.ts';
import { useCreaturesStore } from '@/stores/useCreaturesStore.ts';

const { itemHeight = 55, disableButtons } = defineProps<{
    disableButtons: boolean;
    itemHeight?: number;
}>();

const emits = defineEmits<{
    'add-pressed': [value: CreatureShort];
    'item-pressed': [value: CreatureShort];
}>();

const creaturesStore = useCreaturesStore();
const filteredList = computed(() => [...creaturesStore.creatures].sort((a, b) => a.level - b.level));
const { list, containerProps, wrapperProps } = useVirtualList(filteredList, {
    itemHeight,
});
</script>

<template>
    <div v-bind="containerProps">
        <ul v-bind="wrapperProps">
            <li
                v-for="item in list"
                :key="item.index"
                class="flex flex-row hover:bg-base-100 gap-2 p-1 place-items-center cursor-default"
                :style="`height: ${itemHeight}px`"
                @click="emits('item-pressed', item.data)"
            >
                <div>
                    <div
                        class="text-xl font-semibold text-center bg-base-100 size-8 rounded border-base-300 border-[1px]"
                        :class="`rarity-${item.data.rarity.toLowerCase()}`"
                    >
                        {{ item.data.level }}
                    </div>
                </div>
                <div class="grow overflow-hidden text-nowrap text-ellipsis">
                    <div class="text-lg font-semibold">
                        {{ item.data.name }}
                    </div>
                    <div class="text-xs">{{ item.data.traits.join(', ') }}</div>
                </div>
                <div>
                    <button
                        class="btn btn-sm btn-ghost btn-circle"
                        :disabled="disableButtons"
                        @click="emits('add-pressed', item.data)"
                    >
                        <span class="text-lg mdi mdi-chevron-right"></span>
                    </button>
                </div>
            </li>
        </ul>
    </div>
</template>

<style scoped>
.rarity-unique {
    @apply bg-[var(--unique-color)];
}

.rarity-rare {
    @apply bg-[var(--rare-color)];
}

.rarity-uncommon {
    @apply bg-[var(--uncommon-color)];
}
</style>
