<script setup lang="ts">
import DiceIcon from '@/components/DiceIcon.vue';
import { computed } from 'vue';

const { expression } = defineProps<{
    expression: string
}>();

const parsedExpression = computed(() => {
    // Match dice rolls (e.g., 3d6) and modifiers (e.g., +4, -2)
    const regex = /(\d+)(d)(\d+)|(d)(\d+)|([+-])|(\d+)/g;
    //const regex = /(\d+)d(\d+)|([+-]\s*\d+)|(\d+)/g;
    const matches = expression.matchAll(regex);
    const parsedParts = [];
    for (const match of matches) {
        console.log(match);
        if (match[2] == 'd') {
            // It's a dice roll (e.g., 3d6)
            parsedParts.push({
                type: 'dice',
                count: match[1], // Number of dice (e.g., 3)
                sides: match[3], // Sides of the dice (e.g., 6)
            });
        } else if (match[4] == 'd') {
            // It's a singular dice roll (e.g., d6)
            parsedParts.push({
                type: 'dice',
                count: 1, // Number of dice (in this case always 1)
                sides: match[5], // Sides of the dice (e.g., 6)
            });
        } else if (match[6]) {
            // It's a operation (e.g., + or -)
            parsedParts.push({
                type: 'operation',
                value: match[6].replace(/\s+/g, ''), // Clean up spaces
            });
        } else if (match[7]) {
            // Standalone number (e.g., 4)
            parsedParts.push({
                type: 'number',
                value: match[7],
            });
        }
    }

    return parsedParts;
});

</script>

<template>
    <div class="flex flex-row flex-wrap justify-center items-center">
        <!-- Loop through parsed parts to render them -->
        <div v-for="(part, index) in parsedExpression" :key="index">
            <!-- If it's a dice roll -->
            <template v-if="part.type === 'dice'">
                <div class="flex flex-row items-center">
                    <span v-if="part && part.count && part.count > '1'" class="text-sm font-semibold">{{ part.count }}</span>
                    <DiceIcon class="w-6" :die="`d${part.sides}`" />
                </div>
            </template>

            <!-- If it's a operation or number -->
            <template v-else>
                <span class="text-sm font-semibold">{{ part.value }}</span>
            </template>
        </div>
    </div>
</template>

<style scoped>
/* Optional: Add custom styling for the dice icons or numbers */
</style>