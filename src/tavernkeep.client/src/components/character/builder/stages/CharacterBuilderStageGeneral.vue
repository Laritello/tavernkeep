<template>
    <div class="flex flex-col items-stretch content-stretch gap-6">
        <div>
            <h1 class="font-semibold text-slate-500 uppercase self-start px-1">
                {{ t('builder.stages.general.character.header') }}
            </h1>

            <label class="form-control w-full">
                <div class="label">
                    <span class="label-text">{{ t('builder.stages.general.character.name') }}</span>
                </div>
                <input v-model="name" type="text" placeholder="Unknown hero" class="input input-bordered w-full" />
            </label>

            <label class="form-control w-full">
                <div class="label">
                    <span class="label-text">{{ t('builder.stages.general.character.level') }}</span>
                </div>
                <input v-model="level" type="text" placeholder="Unknown hero" class="input input-bordered w-full" />
            </label>
        </div>

        <div>
            <h1 class="font-semibold text-slate-500 uppercase self-start px-1">
                {{ t('builder.stages.general.ancestry.header') }}
            </h1>

            <label class="form-control w-full">
                <div class="label">
                    <span class="label-text">{{ t('builder.stages.general.ancestry.name') }}</span>
                </div>
                <input v-model="ancestryName" type="text" placeholder="Type here" class="input input-bordered w-full" />
            </label>

            <label class="form-control w-full">
                <div class="label">
                    <span class="label-text">{{ t('builder.stages.general.ancestry.health') }}</span>
                </div>
                <input
                    v-model="ancestryHealth"
                    type="text"
                    placeholder="Type here"
                    class="input input-bordered w-full"
                />
            </label>
        </div>

        <div>
            <h1 class="font-semibold text-slate-500 uppercase self-start px-1">
                {{ t('builder.stages.general.class.header') }}
            </h1>

            <label class="form-control w-full">
                <div class="label">
                    <span class="label-text">{{ t('builder.stages.general.class.name') }}</span>
                </div>
                <input v-model="className" type="text" placeholder="Type here" class="input input-bordered w-full" />
            </label>

            <label class="form-control w-full">
                <div class="label">
                    <span class="label-text">{{ t('builder.stages.general.class.healthPerLevel') }}</span>
                </div>
                <input
                    v-model="classHealthPerLevel"
                    type="text"
                    placeholder="Type here"
                    class="input input-bordered w-full"
                />
            </label>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, watch, type Ref } from 'vue';
import { useI18n } from 'vue-i18n';

import type { Ancestry, Class } from '@/contracts/character';
import type { Character } from '@/entities';
import type { KeyValue } from '@/types';

const { character } = defineProps<{
    character: Character;
}>();

const { t } = useI18n();

const name: Ref<string> = ref(character.name);
const level: Ref<number> = ref(character.level);

const ancestryName: Ref<string> = ref(character.ancestry.name);
const ancestryHealth: Ref<number> = ref(character.ancestry.health);

const className: Ref<string> = ref(character.class.name);
const classHealthPerLevel: Ref<number> = ref(character.class.healthPerLevel);

const emits = defineEmits<{
    update: [value: KeyValue<Character>];
}>();

watch(name, () => emits('update', { key: 'name', value: name.value }));
watch(level, () => emits('update', { key: 'level', value: level.value }));

watch(ancestryName, () =>
    emits('update', { key: 'ancestry', value: { name: ancestryName.value, health: ancestryHealth.value } as Ancestry })
);
watch(ancestryHealth, () =>
    emits('update', { key: 'ancestry', value: { name: ancestryName.value, health: ancestryHealth.value } as Ancestry })
);

watch(className, () =>
    emits('update', {
        key: 'class',
        value: { name: className.value, healthPerLevel: classHealthPerLevel.value } as Class,
    })
);
watch(classHealthPerLevel, () =>
    emits('update', {
        key: 'class',
        value: { name: className.value, healthPerLevel: classHealthPerLevel.value } as Class,
    })
);
</script>
