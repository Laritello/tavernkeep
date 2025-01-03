<script setup lang="ts">
import { ref } from 'vue';
import { type DialogResultCallback } from '@/composables/useModal';
import type { Skill } from '@/contracts/character';
import { Proficiency, SkillType } from '@/contracts/enums';
import SkillEditView from '@/components/character/SkillEditView.vue';

const { closeModal, skills } = defineProps<{
    skills: Record<SkillType, Skill>;
    closeModal: DialogResultCallback<Record<SkillType, Proficiency>>;
}>();

const currentSkills = ref({ ...skills });

function confirm() {
    const payload = {  } as Record<SkillType, Proficiency>;
    for (const skill of Object.values(currentSkills.value)) {
        payload[skill.type] = skill.proficiency;
    }
    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}

function updateSkill(skill: Skill) {
    currentSkills.value[skill.type] = skill;
}
</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">Edit skills</h3>
            <form @submit.prevent="confirm" method="dialog" class="space-x-2">
                <div class="flex flex-col p-2 w-full">
                    <div class="flex flex-col">
                        <SkillEditView v-for="skill in Object.values(currentSkills)" :skill="skill" :key="skill.type" @onChange="updateSkill" />
                    </div>
                </div>
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">Save</button>
                    <button @click="cancel" class="btn w-24" type="button">Cancel</button>
                </div>
            </form>
        </div>
    </dialog>
</template>
