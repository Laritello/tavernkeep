<template>
    <div class="flex flex-col items-center content-stretch">
        <h1 class="font-semibold text-slate-500 uppercase self-start">Character</h1>

        <label class="form-control w-full max-w-xs">
            <div class="label">
                <span class="label-text">Name</span>
            </div>
            <input type="text" placeholder="Unknown hero" class="input input-bordered w-full max-w-xs" v-model="name" />
        </label>

        <h1 class="font-semibold mt-4 text-slate-500 uppercase self-start">Ancestry</h1>

        <label class="form-control w-full max-w-xs">
            <div class="label">
                <span class="label-text">Name</span>
            </div>
            <input type="text" placeholder="Type here" class="input input-bordered w-full max-w-xs"
                v-model="ancestryName" />
        </label>

        <label class="form-control w-full max-w-xs">
            <div class="label">
                <span class="label-text">Health</span>
            </div>
            <input type="text" placeholder="Type here" class="input input-bordered w-full max-w-xs"
                v-model="ancestryHealth" />
        </label>

        <h1 class="font-semibold mt-4 text-slate-500 uppercase self-start">Class</h1>

        <label class="form-control w-full max-w-xs">
            <div class="label">
                <span class="label-text">Name</span>
            </div>
            <input type="text" placeholder="Type here" class="input input-bordered w-full max-w-xs"
                v-model="className" />
        </label>

        <label class="form-control w-full max-w-xs">
            <div class="label">
                <span class="label-text">Health per level</span>
            </div>
            <input type="text" placeholder="Type here" class="input input-bordered w-full max-w-xs"
                v-model="classHealthPerLevel" />
        </label>
    </div>
</template>

<script setup lang="ts">
import type { Ancestry, Class } from '@/contracts/character';
import type { Character } from '@/entities';
import { ref, watch, type Ref } from 'vue';

const { character } = defineProps<{
    character: Character
}>();

const name: Ref<string> = ref(character.name);

const ancestryName: Ref<string> = ref(character.ancestry.name);
const ancestryHealth: Ref<number> = ref(character.ancestry.health);

const className: Ref<string> = ref(character.class.name);
const classHealthPerLevel: Ref<number> = ref(character.class.healthPerLevel);

const emits = defineEmits<{
    update: [value: { key: keyof Character; value: unknown }]
}>();

watch(name, () => emits("update", { key: "name", value: name.value }));

watch(ancestryName, () => emits("update", { key: "ancestry", value: { name: ancestryName.value, health: ancestryHealth.value } as Ancestry }));
watch(ancestryHealth, () => emits("update", { key: "ancestry", value: { name: ancestryName.value, health: ancestryHealth.value } as Ancestry }));

watch(className, () => emits("update", { key: "class", value: { name: className.value, healthPerLevel: classHealthPerLevel.value } as Class }));
watch(classHealthPerLevel, () => emits("update", { key: "class", value: { name: className.value, healthPerLevel: classHealthPerLevel.value } as Class }));

</script>