<template>
    <div class="card card-body bg-neutral text-neutral-content w-full lg:max-w-md">
        <h1 class="card-title self-center">Abilities</h1>
        <div>
            <table class="table">
                <!-- head -->
                <thead class="text-neutral-content">
                <tr>
                    <th>Ability</th>
                    <th>Score</th>
                    <th>Modifier</th>
                </tr>
                </thead>
                <tbody>
                <AbilityView
                    v-for="ability in abilities"
                    :ability="ability"
                    :key="ability.type"
                />
                </tbody>
            </table>
        </div>
    </div>
</template>
<script setup lang="ts">
import type { Ability } from '@/contracts/character';

import AbilityView from './AbilityView.vue';
import type { AbilityType } from '@/contracts/enums';
import SkillView from '@/components/character/SkillView.vue';

const { abilities } = defineProps<{ abilities: Record<AbilityType, Ability> }>();
</script>
