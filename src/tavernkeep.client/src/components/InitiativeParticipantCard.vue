<template>
    <div
        class="card card-compact"
        :class="[
            participant.isActive ? 'border-2 border-accent animate-pulse' : 'border border-base-300',
            participant.type === 'player' ? 'bg-primary bg-opacity-10' : 'bg-error bg-opacity-10',
        ]"
    >
        <div class="card-body flex-row items-center gap-4">
            <!-- Initiative Value -->
            <div class="text-3xl font-bold w-12 text-center">
                {{ participant.initiative }}
            </div>

            <!-- Participant Info -->
            <div class="flex-1">
                <h3 class="font-bold">{{ participant.name }}</h3>
                <div class="text-sm opacity-70">
                    {{ participant.type === 'player' ? participant.class : participant.type }}
                </div>
            </div>

            <!-- HP Display -->
            <div class="badge badge-lg" :class="participant.type === 'player' ? 'badge-primary' : 'badge-error'">
                HP: {{ participant.currentHp }}/{{ participant.maxHp }}
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

<script setup lang="ts">
import type { CombatParticipant } from '@/types/combat';

const { participant } = defineProps<{
    participant: CombatParticipant;
}>();

defineEmits<{
    (e: 'edit', participant: CombatParticipant): void;
    (e: 'remove', id: string): void;
}>();
</script>
