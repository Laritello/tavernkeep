<script setup lang="ts">
import type { Proficiency } from '@/contracts/enums';

const { proficiency } = defineProps<{ proficiency: Proficiency }>();
const values: Record<Proficiency, number> = {
    Untrained: 0,
    Trained: 2,
    Expert: 4,
    Master: 6,
    Legendary: 8,
};
</script>

<template>
    <div class="min-w-fit">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="-0.8 -1 39.3 17" class="w-16" stroke="oklch(var(--n))">
            <rect
                transform="rotate(45)"
                x="5.3033"
                y="-5.3033"
                width="10.6066017"
                height="10.6066017"
                :class="values[proficiency] >= values['Trained'] ? 'proficient' : 'not-proficient'"
                stroke-width="1"
            />
            <rect
                transform="rotate(45)"
                x="10.6066017"
                y="-10.6066017"
                width="10.6066017"
                height="10.6066017"
                :class="values[proficiency] >= values['Expert'] ? 'proficient' : 'not-proficient'"
                stroke-width="1"
            />
            <rect
                transform="rotate(45)"
                x="15.9099017"
                y="-15.9099017"
                width="10.6066017"
                height="10.6066017"
                :class="values[proficiency] >= values['Master'] ? 'proficient' : 'not-proficient'"
                stroke-width="1"
            />
            <rect
                transform="rotate(45)"
                x="21.2132017"
                y="-21.2132017"
                width="10.6066017"
                height="10.6066017"
                :class="values[proficiency] >= values['Legendary'] ? 'proficient' : 'not-proficient'"
                stroke-width="1"
            />
        </svg>
    </div>
</template>
<style scoped>
.proficient {
    fill: #efbf04;
}
.not-proficient {
    fill: oklch(var(--b3));
}
</style>
