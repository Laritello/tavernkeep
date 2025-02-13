<script setup lang="ts">
import type { Participant } from '@/contracts/encounter/Participant.ts';

const { participant, activeTurn } = defineProps<{
    participant: Participant;
    activeTurn: boolean;
}>();

defineEmits<{
    (e: 'edit', participant: Participant): void;
    (e: 'remove', id: string): void;
}>();
</script>

<template>
    <div
        class="card card-compact"
        :class="[
            activeTurn ? 'border-2 border-accent animate-pulse' : 'border border-base-300',
            participant.type === 'Character' ? 'bg-primary bg-opacity-10' : 'bg-error bg-opacity-10',
        ]"
    >
        <div class="card-body flex-row items-center gap-4">
            <!-- Initiative Value -->
            <div class="text-3xl font-bold w-12 text-center">
                {{ participant.initiative }}
            </div>

            <!-- Participant Info -->
            <div class="flex-1">
                <h3 class="font-bold">{{ participant.id.slice(0, 6) }}</h3>
                <div class="text-sm opacity-70">CLASS / TYPE</div>
            </div>

            <!-- HP Display -->
            <div class="badge badge-lg" :class="participant.type === 'Character' ? 'badge-primary' : 'badge-error'">
                HP:
                {{ participant.type === 'Character' ? '92/100' : '79%' }}
            </div>

            <!-- Action Buttons -->
            <div class="flex gap-2">
                <button class="btn btn-circle btn-sm btn-ghost" @click="$emit('edit', participant)">
                    <span class="mdi mdi-pencil"></span>
                </button>
                <button class="btn btn-circle btn-sm btn-ghost text-error" @click="$emit('remove', participant.id)">
                    <span class="mdi mdi-delete"></span>
                </button>
            </div>
        </div>
    </div>
</template>
