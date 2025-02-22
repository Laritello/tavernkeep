<script setup lang="ts">
import type { Character } from '@/entities';
import { useCharacters } from '@/stores/characters.js';

const { disableButtons } = defineProps<{
    disableButtons: boolean;
}>();

const emits = defineEmits<{
    'add-pressed': [value: Character];
}>();

const charactersStore = useCharacters();
</script>

<template>
    <ul class="overflow-auto">
        <li
            v-for="character in charactersStore.all"
            :key="character.id"
            class="flex flex-row hover:bg-base-100 gap-2 p-1 place-items-center cursor-default"
        >
            <div>
                <div class="text-xl font-semibold text-center bg-base-100 size-8 rounded border-base-300 border-[1px]">
                    {{ character.level }}
                </div>
            </div>
            <div class="grow overflow-hidden text-nowrap text-ellipsis">
                <div class="text-lg font-semibold">
                    {{ character.name }}
                </div>
                <div class="text-xs">{{ character.class.name }}</div>
            </div>
            <div>
                <button
                    class="btn btn-sm btn-ghost btn-circle"
                    :disabled="disableButtons"
                    @click="emits('add-pressed', character)"
                >
                    <span class="text-lg mdi mdi-chevron-right"></span>
                </button>
            </div>
        </li>
    </ul>
</template>
