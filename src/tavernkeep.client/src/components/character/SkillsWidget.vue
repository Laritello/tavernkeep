<template>
    <div class="card card-body bg-neutral text-neutral-content w-auto">
        <h1 class="card-title self-center">Skills</h1>
        <div>
            <table class="table">
                <!-- head -->
                <thead class="text-neutral-content">
                    <tr>
                        <th></th>
                        <th>Skill</th>
                        <th>Bonus</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <SkillView
                        v-for="skill in skills"
                        :skill="skill"
                        :key="skill.type"
                        @roll="(value) => console.log(value.type)"
                    />
                </tbody>
            </table>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { Skill } from '@/contracts/character';
import SkillView from './SkillView.vue';

const { skills } = defineProps<{ skills: Record<string, Skill> }>();
</script>
