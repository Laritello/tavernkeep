<template>
    <div class="container mx-auto p-4">
        <!-- Round Counter -->
        <div class="flex justify-between items-center mb-6">
            <h1 class="text-2xl font-bold">Encounter Builder</h1>
            <div class="badge badge-lg">Round {{ currentRound }}</div>
        </div>

        <div class="grid grid-cols-2 gap-4">
            <!-- Characters Section -->
            <aside class="flex flex-col gap-4 col-span-1 bg-base-200">
                <div class="card-body">
                    <h2 class="card-title">Characters</h2>
                    <div class="flex flex-col gap-2">
                        <div v-for="character in players" :key="character.id" class="flex items-baseline gap-2">
                            <div class="avatar placeholder">
                                <div class="bg-neutral text-neutral-content rounded-full w-8">
                                    <span class="text-xs">{{ character.name?.charAt(0) }}</span>
                                </div>
                            </div>
                            <span>{{ character.name }}</span>
                            <button
                                class="btn btn-circle btn-sm btn-accent mt-4"
                                @click="addCharacterToInitiative(character)"
                            >
                                <span class="mdi mdi-plus"></span>
                            </button>
                        </div>
                    </div>
                </div>

                <!-- Monsters Section -->
                <div class="col-span-2 bg-base-200 shadow-xl">
                    <div class="card-body">
                        <h2 class="card-title">Monsters</h2>
                        <div class="flex flex-col gap-2">
                            <div v-for="monster in monsters" :key="monster.id" class="flex items-baseline gap-2">
                                <div class="avatar placeholder">
                                    <div class="bg-error text-error-content rounded-full w-8">
                                        <span class="text-xs">M</span>
                                    </div>
                                </div>
                                <span>{{ monster.name }}</span>
                                <button
                                    class="btn btn-circle btn-sm btn-accent mt-4"
                                    @click="addMonsterToInitiative(monster)"
                                >
                                    <span class="mdi mdi-plus"></span>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </aside>

            <!-- Initiative Tracker -->
            <div class="card bg-base-300 shadow-xl">
                <div class="card-body">
                    <h2 class="card-title">Initiative Tracker</h2>
                    <div class="divider">Initiative Order</div>
                    <VueDraggable
                        v-model="initiativeOrder"
                        class="flex flex-col gap-2 min-h-52"
                        :animation="150"
                        @start="drag = true"
                        @end="nextTick(() => (drag = false))"
                    >
                        <TransitionGroup :name="!drag ? 'fade' : undefined" type="transition">
                            <InitiativeParticipantCard
                                v-for="creature in initiativeOrder"
                                :key="creature.id"
                                class="flex items-center justify-between cursor-pointer"
                                :participant="creature"
                                @edit="console.log('edit participant card')"
                                @remove="removeFromInitiative(creature)"
                            />
                        </TransitionGroup>
                    </VueDraggable>
                    <div class="card-actions justify-end mt-4">
                        <button v-if="!isCombatActive" class="btn btn-primary" @click="startCombat">
                            Start Combat
                        </button>
                        <button v-else class="btn btn-accent" @click="nextTurn">Next Turn</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia';
import { nextTick, ref } from 'vue';
import { VueDraggable } from 'vue-draggable-plus';

import InitiativeParticipantCard from '@/components/InitiativeParticipantCard.vue';
import { useCombatStore } from '@/stores/combat.ts';
import type { CombatParticipant, Monster, Player } from '@/types/combat.ts';

const drag = ref(false);
const combatStore = useCombatStore();
const { isCombatActive, currentRound, initiativeOrder, players, monsters } = storeToRefs(combatStore);

const addCharacterToInitiative = (player: Player) => {
    combatStore.addToInitiative(player);
};

const addMonsterToInitiative = (monsterTemplate: Monster) => {
    const monster = { ...monsterTemplate, id: Math.random().toString(36).substring(2, 12) };
    combatStore.addToInitiative(monster);
};

const removeFromInitiative = (participant: CombatParticipant) => combatStore.removeFromInitiative(participant);

const startCombat = () => {
    // Start combat
};

const nextTurn = () => {
    // Move to next creature in initiative order
    // Increment round counter when full round is complete
};
</script>

<!--suppress CssUnusedSymbol -->
<style scoped>
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.15s ease;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}
</style>
