import { defineStore } from 'pinia';
import { ref, computed, watch } from 'vue';

import { useCharacters } from '@/stores/characters.ts';
import { useUsers } from '@/stores/users.ts';
import type { Player, Monster, CombatParticipant } from '@/types/combat';

export const useCombatStore = defineStore('combat', () => {
    const usersStore = useUsers();
    const charactersStore = useCharacters();

    const characters = computed(() => {
        const users = Object.values(usersStore.dictionary);

        if (Object.keys(charactersStore.all).length === 0) {
            return [];
        }

        return users
            .filter((user) => !!user.activeCharacterId)
            .map((user) => charactersStore.all[user.activeCharacterId]);
    });

    const players = computed(() => {
        return characters.value.map((c): Player => {
            console.log(c);
            return {
                type: 'player',
                class: c.class.name,
                name: c.name,
                id: c.id,
                level: c.level,
                maxHp: c.health.max,
                currentHp: c.health.current,
                isActive: false,
                initiative: -1,
            };
        });
    });

    const monsters = ref<Monster[]>([
        {
            id: '0',
            type: 'monster',
            name: 'Goblin',
            challengeRating: 1,
            maxHp: 10,
            currentHp: 10,
            initiative: -1,
            isActive: false,
        },
        {
            id: '1',
            type: 'monster',
            name: 'Kobold',
            challengeRating: -1,
            maxHp: 10,
            currentHp: 10,
            initiative: -1,
            isActive: false,
        },
        {
            id: '2',
            type: 'monster',
            name: 'Lizard',
            challengeRating: -1,
            maxHp: 10,
            currentHp: 10,
            initiative: -1,
            isActive: false,
        },
    ]);
    const initiativeOrder = ref<CombatParticipant[]>([]);
    const currentRound = ref(0);
    const isCombatActive = ref(false);
    const currentTurnIndex = ref(0);

    const currentParticipant = computed(() => initiativeOrder.value[currentTurnIndex.value]);

    const addToInitiative = (participant: CombatParticipant) => initiativeOrder.value.push(participant);
    const removeFromInitiative = (participant: CombatParticipant) => {
        const index = initiativeOrder.value.indexOf(participant);
        initiativeOrder.value.splice(index, 1);
    };

    // Watch for initiative order changes
    watch(
        initiativeOrder,
        (newOrder) => {
            // Update active states after reordering
            newOrder.forEach((participant, index) => {
                participant.isActive = index === currentTurnIndex.value;
            });
        },
        { deep: true }
    );

    return {
        players,
        monsters,
        initiativeOrder,
        currentRound,
        isCombatActive,
        currentTurnIndex,
        currentParticipant,
        addToInitiative,
        removeFromInitiative,
    };
});
