<template>
    <div class="rounded bg-[#867d284b] divide-y">
        <div class="flex justify-between px-2 pt-1">
            <div>{{ message.sender.login }}</div>
            <div>{{ message.rollType }}</div>
            <div class="text-xs font-light opacity-50">{{ formatDate(message.created) }}</div>
        </div>
        <div class="flex">
            <DiceIcon die="d20" class="w-16 m-2" />
            <div class="flex flex-col w-full justify-evenly">
                <div>
                    <span class="text-red-700 link">{{ message.sender.login }}</span> performs a
                    <span
                        class="tooltip link text-green-700"
                        :data-tip="`${message.skill.proficiency}: ${message.result.modifier}`"
                    >
                        {{ message.skill.type }}
                    </span>
                    check
                </div>
            </div>
        </div>
        <div class="flex text-3xl justify-center p-4">
            <span
                :data-tip="`d20[${message.result.results[0].value}] ${message.result.modifier}`"
                class="tooltip cursor-default text-shadow"
                :class="{
                    'text-success': message.result.results[0].value == 20,
                    'text-error': message.result.results[0].value == 1,
                }"
                >{{ message.result.value }}</span
            >
        </div>
    </div>
</template>
<script setup lang="ts">
import DiceIcon from '@/components/DiceIcon.vue';
import { SkillRollMessage } from '@/entities/Message';
import { onMounted } from 'vue';
const { message } = defineProps<{
    message: SkillRollMessage;
}>();

onMounted(() => console.log(message));

function formatDate(dateString: Date): string {
    const date = new Date(dateString.toString());
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const seconds = date.getSeconds().toString().padStart(2, '0');

    return `${hours}:${minutes}:${seconds}`;
}
</script>
<style scoped>
.text-shadow {
    text-shadow: rgba(0, 0, 0, 0.333) 0px 0px 3px;
}
</style>
