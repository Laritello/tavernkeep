<script setup lang="ts">
import HealthBar from '@/components/character/HealthBar.vue';
import StatblockDialog from '@/components/dialogs/StatblockDialog.vue';
import { useModal } from '@/composables/useModal';
import type { ConditionShortDto } from '@/contracts/conditions/ConditionShortDto.ts';
import type { Participant } from '@/contracts/encounter/Participant.ts';

const { participant, activeTurn } = defineProps<{
    participant: Participant;
    activeTurn: boolean;
}>();

const emits = defineEmits<{
    (e: 'edit', target: Participant): void;
    (e: 'remove', targetId: string): void;
    (e: 'add-condition', targetId: string, conditionName: string): void;
    (e: 'remove-condition', targetId: string, conditionName: string): void;
    (e: 'edit-condition', targetId: string, conditionName: string, conditionLevel: number): void;
}>();

function onDrop(event: DragEvent) {
    if (event.dataTransfer === null) {
        return;
    }

    const conditionName = event.dataTransfer.getData('condition');
    if (conditionName) {
        emits('add-condition', participant.id, conditionName);
    }
}

function onEditCondition(condition: ConditionShortDto, delta: number) {
    const conditionLevel = Math.max(condition.level! + delta, 1);
    if (conditionLevel === condition.level) {
        return;
    }
    emits('edit-condition', participant.id, condition.name, conditionLevel);
}

async function showStatblock() {
    const modal = useModal();
    const result = await modal.show(StatblockDialog, { data: participant.statblock });
    console.log(result);
}
</script>

<template>
    <div
        class="rounded-md"
        :class="[
            activeTurn ? 'border-2 border-accent' : 'border p-[1px] border-base-300',
            participant.type === 'Character' ? 'bg-primary bg-opacity-10' : 'bg-error bg-opacity-10',
        ]"
        @drop="onDrop($event)"
        @dragover.prevent
        @dragenter.prevent
    >
        <div class="flex flex-row items-center">
            <div class="drag-handle p-1 cursor-grab">
                <span class="mdi mdi-drag text-xl"></span>
            </div>
            <div class="grow">
                <!-- Stats -->
                <div class="grid grow grid-cols-[3rem_1fr_7rem_3rem_3rem] p-2 items-center gap-4">
                    <!-- Initiative Value -->
                    <div
                        class="flex input input-bordered justify-center items-center size-12 p-0 cursor-default hover:border-2"
                    >
                        <span class="text-center font-semibold text-lg">
                            {{ participant.initiative ?? '--' }}
                        </span>
                    </div>

                    <!-- Participant Info -->
                    <div class="flex flex-col">
                        <div class="flex flex-row">
                            <h3 class="font-bold" @click="showStatblock">{{ participant.name }}</h3>
                            <button class="btn btn-circle btn-sm btn-ghost" @click="emits('edit', participant)">
                                <span class="mdi mdi-pencil"></span>
                            </button>
                            <button class="btn btn-circle btn-sm btn-ghost">
                                <span class="mdi mdi-eye"></span>
                            </button>
                            <button
                                class="btn btn-circle btn-sm btn-ghost text-error"
                                @click="emits('remove', participant.id)"
                            >
                                <span class="mdi mdi-delete"></span>
                            </button>
                        </div>
                        <!-- HP Display -->
                        <HealthBar :health="participant.health" width="100%" height="1.25rem" />
                    </div>

                    <!-- Saves & Perception -->
                    <div class="bg-base-100 rounded-md p-2 text-sm">
                        <div v-for="[name, value] in Object.entries(participant.savingThrows)" :key="name">
                            {{ name }}: {{ value }}
                        </div>
                    </div>

                    <!-- Perception -->
                    <div class="bg-base-100 rounded-md p-2 text-center">
                        <div class="mdi mdi-eye"></div>
                        {{ participant.perception }}
                    </div>

                    <!-- Armor -->
                    <div class="bg-base-100 rounded-md p-2 text-center">
                        <div class="mdi mdi-shield"></div>
                        {{ participant.armorClass }}
                    </div>
                </div>
                <!-- Conditions -->
                <ul v-if="participant.conditions.length > 0" class="flex flex-wrap gap-1 pb-1">
                    <li
                        v-for="condition in participant.conditions"
                        :key="condition.name"
                        class="group condition-badge gap-1 cursor-default"
                    >
                        <span>{{ condition.name }}</span>
                        <div
                            v-if="condition.hasLevels"
                            class="hidden h-full bg-base-200/25 hover:bg-base-200/50 aspect-square rounded-full items-center justify-center group-hover:flex"
                            @click="onEditCondition(condition, -1)"
                        >
                            <span class="mdi mdi-minus text-xs"></span>
                        </div>

                        <span>{{ condition.level }}</span>
                        <div
                            v-if="condition.hasLevels"
                            class="hidden h-full bg-base-200/25 hover:bg-base-200/50 aspect-square rounded-full items-center justify-center group-hover:flex"
                            @click="onEditCondition(condition, 1)"
                        >
                            <span class="mdi mdi-plus text-xs"></span>
                        </div>
                        <div
                            class="hidden h-full bg-base-200/25 hover:bg-base-200/50 aspect-square rounded-full items-center justify-center group-hover:flex"
                            @click="emits('remove-condition', participant.id, condition.name)"
                        >
                            <span class="mdi mdi-delete text-xs"></span>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>
<style scoped>
.condition-badge {
    @apply badge badge-error badge-sm;
}
</style>
