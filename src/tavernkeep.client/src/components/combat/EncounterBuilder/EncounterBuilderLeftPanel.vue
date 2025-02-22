<script setup lang="ts">
import { computed, useTemplateRef } from 'vue';

import EncounterCharacterList from '@/components/combat/EncounterBuilder/EncounterCharacterList.vue';
import CreatureList from '@/components/library/CreatureList.vue';
import TabMenu from '@/components/shared/TabMenu.vue';
import type { CreatureShort } from '@/contracts/creatures/CreatureShort.ts';
import { ParticipantType } from '@/contracts/enums';
import type { Character } from '@/entities';
import { useCurrentEncounterStore } from '@/stores/useCurrentEncounterStore.ts';

const currentEncounterStore = useCurrentEncounterStore();
const tabMenuRef = useTemplateRef<HTMLDivElement>('tab-menu');
const tabMenuHeight = computed(() => (tabMenuRef.value?.offsetHeight ?? 300) - 40);

async function addPlayerCharacter(character: Character) {
    if (!currentEncounterStore.isActive) {
        return;
    }

    await currentEncounterStore.addParticipant({
        type: ParticipantType.Character,
        entityId: character.id,
    });
}

async function addCreature(creature: CreatureShort) {
    if (!currentEncounterStore.isActive) {
        return;
    }

    await currentEncounterStore.addParticipant({
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
                ]"
                default-tab="characters"
                variant="bordered"
                class="h-full"
            >
                <template #characters>
                    <EncounterCharacterList
                        :disable-buttons="!currentEncounterStore.isActive"
                        class="w-full h-[calc(100%_-_40px)]"
                        @add-pressed="addPlayerCharacter"
                    />
                </template>

                <template #creatures>
                    <CreatureList
                        :disable-buttons="!currentEncounterStore.isActive"
                        class="w-full h-[calc(100%_-_40px)]"
                        @add-pressed="addCreature"
                    />
                </template>
            </TabMenu>
        </div>
    </aside>
</template>

<style scoped></style>
