import { defineStore } from 'pinia';
import { computed, ref } from 'vue';

import { AncestryType, ClassType } from '@/contracts/enums';
import { CharacterTemplateFactory } from '@/factories/CharacterTemplateFactory.ts';

export const useCharacterBuilderStore = defineStore('character-builder', () => {
    const template = ref(
        CharacterTemplateFactory.createCharacterTemplate('Unknown hero', 1, AncestryType.Human, ClassType.Fighter)
    );

    const ancestryName = computed({
        get: () => template.value.ancestry.name,
        set: (value) => {
            const isKnownAncestry = value in AncestryType;
            if (isKnownAncestry) {
                const newAncestryTemplate = CharacterTemplateFactory.createAncestryTemplate(value as AncestryType);
                Object.assign(template.value, newAncestryTemplate);
            }
        },
    });

    const className = computed({
        get: () => template.value.class.name,
        set: (value) => {
            const isKnownClass = value in ClassType;
            if (isKnownClass) {
                const newClassTemplate = CharacterTemplateFactory.createClassTemplate(value as ClassType);
                Object.assign(template.value, newClassTemplate);
            }
        },
    });

    const reset = () => {
        template.value = CharacterTemplateFactory.createCharacterTemplate(
            'Unknown hero',
            1,
            AncestryType.Human,
            ClassType.Fighter
        );
    };

    return { template, ancestryName, className, reset };
});
