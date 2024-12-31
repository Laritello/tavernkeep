<script setup lang="ts">
import type { Proficiency } from '@/contracts/enums';

const { proficiency } = defineProps<{ proficiency: Proficiency }>();
const values: Record<Proficiency, number> =
{
    Untrained: 0,
    Trained: 2,
    Expert: 4,
    Master: 6,
    Legendary: 8
};

</script>

<template>
    <div class="min-w-fit">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="-0.8 -1 39.3 17" class="w-16" stroke="currentColor">
            <rect transform="rotate(45)" x="5.3033" y="-5.3033" width="10.6066017" height="10.6066017"
                :fill="values[proficiency] >= values['Trained'] ? '#efbf04' : '#FFFFFF'" stroke-width="1" />
            <rect transform="rotate(45)" x="10.6066017" y="-10.6066017" width="10.6066017" height="10.6066017"
            :fill="values[proficiency] >= values['Expert'] ? '#efbf04' : '#FFFFFF'" stroke-width="1" />
            <rect transform="rotate(45)" x="15.9099017" y="-15.9099017" width="10.6066017" height="10.6066017"
            :fill="values[proficiency] >= values['Master'] ? '#efbf04' : '#FFFFFF'" stroke-width="1" />
            <rect transform="rotate(45)" x="21.2132017" y="-21.2132017" width="10.6066017" height="10.6066017"
            :fill="values[proficiency] >= values['Legendary'] ? '#efbf04' : '#FFFFFF'" stroke-width="1" />
        </svg>
    </div>
</template>
<style scoped></style>