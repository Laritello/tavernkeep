<template>
    <v-sheet :color="color || 'green'" rounded>
        <div class="text-container px-2 pb-1">
            <div class="header px-1 pt-1">
                <div class="body-1 font-weight-medium">{{ message.sender.login }}</div>
                <div class="body-1 font-weight-light">{{ formatDate(message.created) }}</div>
            </div>
            <div>
                <div class="body-1 pl-1">
                    Roll type: {{ message.rollType.toString() }}; Roll result: {{ message.result.value }}
                </div>
            </div>
        </div>
    </v-sheet>
</template>
<script setup lang="ts">
import { RollMessage } from '@/entities/Message';
const { message } = defineProps<{
    message: RollMessage;
    color?: string;
}>();

function formatDate(dateString: Date): string {
    const date = new Date(dateString.toString());
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const seconds = date.getSeconds().toString().padStart(2, '0');

    return `${hours}:${minutes}:${seconds}`;
}
</script>
<style scoped>
.text-container {
    display: grid;
    grid-template-rows: min-content min-content 1fr;
    grid-gap: 0px;
}

.text-container .header {
    display: grid;
    grid-template-columns: 1fr min-content;
    grid-gap: 3px;
    align-items: center;
}
</style>
