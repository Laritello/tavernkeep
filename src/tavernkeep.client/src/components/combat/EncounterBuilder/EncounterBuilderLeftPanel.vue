<script setup lang="ts">
import EncounterCharacterList from '@/components/combat/EncounterBuilder/EncounterCharacterList.vue';
import EncounterConditionsList from '@/components/combat/EncounterBuilder/EncounterConditionsList.vue';
import CreatureList from '@/components/library/CreatureList.vue';
import TabMenu from '@/components/shared/TabMenu.vue';
import type { CreatureShort } from '@/contracts/creatures/CreatureShort.ts';
import { ParticipantType } from '@/contracts/enums';
import type { Character } from '@/entities';
import { useEncountersStore } from '@/stores/useEncountersStore.ts';

const encountersStore = useEncountersStore();

async function addPlayerCharacter(character: Character) {
    if (!encountersStore.selectedEncounterId) {
        return;
    }

    await encountersStore.addParticipant(encountersStore.selectedEncounterId, {
        type: ParticipantType.Character,
        entityId: character.id,
    });
}

async function addCreature(creature: CreatureShort) {
    if (!encountersStore.selectedEncounterId) {
        return;
    }

    await encountersStore.addParticipant(encountersStore.selectedEncounterId, {
        type: ParticipantType.Creature,
        entityId: creature.id,
    });
}
</script>

<template>
    <aside class="bg-base-100">
        <div ref="tab-menu" class="w-full h-full">
            <TabMenu
                :tabs="[
                    { id: 'characters', label: 'Characters' },
                    { id: 'creatures', label: 'Creatures' },
                    { id: 'conditions', label: 'Conditions' },
                ]"
                default-tab="characters"
                variant="bordered"
                class="h-full w-full"
            >
                <template #characters>
                    <EncounterCharacterList
                        :disable-buttons="!encountersStore.selectedEncounterId"
                        class="w-full h-[calc(100%_-_40px)]"
                        @add-pressed="addPlayerCharacter"
                    />
                </template>

                <template #creatures>
                    <CreatureList
                        :disable-buttons="!encountersStore.selectedEncounterId"
                        class="w-full h-[calc(100%_-_40px)]"
                        @add-pressed="addCreature"
                    />
                </template>

                <template #conditions>
                    <EncounterConditionsList
                        :disable-buttons="!encountersStore.selectedEncounterId"
                        class="w-full h-[calc(100%_-_40px)] py-2"
                    />
                </template>
            </TabMenu>
        </div>
    </aside>
</template>

<style scoped></style>
