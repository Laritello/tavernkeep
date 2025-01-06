<template>
    <div class="chat" :class="{ 'chat-end': alignRight, 'chat-start': !alignRight, }">
        <!--Hack to make it align with headers in other messages-->
        <div class="chat-header" :class="{ 'mr-10': alignRight, 'ml-10': !alignRight, }">
            {{ message.displayName }}
            <time class="text-xs opacity-50">{{ formatDate(message.created) }}</time>
        </div>
        <div class="roll-bubble rounded-3xl bg-neutral relative col-span-2 justify-items-center">
            <!--Avatar and its border-->
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 10 10" class="absolute -top-1 w-12 h-12"
                :class="{ '-right-1': alignRight, '-left-1': !alignRight, }">
                <circle cx="5" cy="5" r="5" fill="oklch(var(--b1))" />
            </svg>
            <div class="avatar placeholder absolute top-0" :class="{ 'right-0': alignRight, 'left-0': !alignRight, }">
                <div class="bg-neutral text-neutral-content w-10 rounded-full">
                    <span>{{ message.sender.login.slice(0, 2) }}</span>
                </div>
            </div>

            <!--Tag for secret and private rolls-->
            <div v-if="message.rollType != RollType.Public"
                class="absolute top-2 bg-base-100 py-1 px-2 text-xs text-neutral dark:text-neutral-content rounded-xl tracking-tighter"
                :class="{ 'left-2': alignRight, 'right-2': !alignRight, }">
                <div>{{ message.rollType }}</div>
            </div>

            <!--Roll header and subheader-->
            <div class="flex flex-col">
                <p class="text-center uppercase leading-3">
                    <span class="text-md font-bold tracking-wide" :class="{
                        'custom': rollMessageParameters.header == 'Custom',
                        'skill-check': rollMessageParameters.header == 'Check',
                        'saving-throw': rollMessageParameters.header == 'Save',
                    }">
                        {{ rollMessageParameters.header }}
                    </span>
                    <br />
                    <span class="text-xs font-normal tracking-wide">{{ rollMessageParameters.subHeader }}</span>
                </p>
            </div>

            <!--Result of the roll-->
            <div class="flex text-5xl pt-1 text-neutral-content">
                <span :data-tip="message.expression" class="tooltip">{{ message.result.value }}</span>
            </div>

            <!--Dice notation for the roll-->
            <DiceExpression class="my-3 min-w-60 max-w-60" :expression="message.expression" />

            <!--Show more toggle-->
            <label class="swap swap-rotate">
                <!-- this hidden checkbox controls the state -->
                <input type="checkbox" name="input-roll-toggle" v-model="rollsClosed" />

                <!-- sun icon -->
                <svg class="swap-on h-6 w-6 fill-current" xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960">
                    <path d="M480-344 240-584l56-56 184 184 184-184 56 56-240 240Z" />
                </svg>

                <!-- moon icon -->
                <svg class="swap-off h-6 w-6 fill-current" xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960">
                    <path d="M480-528 296-344l-56-56 240-240 240 240-56 56-184-184Z" />
                </svg>
            </label>

            <!--List of rolls results-->
            <div class="collapse" :class="{ 'collapse-close': rollsClosed, 'collapse-open': !rollsClosed, }">
                <div class="collapse-content p-0 max-w-60">
                    <div class="flex flex-row flex-wrap gap-x-1 justify-center">
                        <DiceIcon v-for="result in message.result.results" :key="result.value" :die="result.type"
                            :value="result.value" class="w-4" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { RollType } from '@/contracts/enums';
import type { RollMessage } from '@/entities/Message';
import DiceExpression from '@/components/DiceExpression.vue';
import { ref } from 'vue';
import DiceIcon from '@/components/DiceIcon.vue';

interface RollMessageParameters {
    header: string;
    subHeader: string;
}

const { message, rollMessageParameters } = defineProps<{
    message: RollMessage;
    alignRight: boolean;
    rollMessageParameters: RollMessageParameters;
}>();

const rollsClosed = ref(true);

function formatDate(dateString: Date): string {
    const date = new Date(dateString.toString());
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');

    return `${hours}:${minutes}`;
}
</script>
<style scoped>
.chat-end .roll-bubble {
    grid-column-start: 1;
}

.chat-start .roll-bubble {
    grid-column-start: 2;
}

.roll-bubble {
    position: relative;
    display: block;
    width: -moz-fit-content;
    width: fit-content;
    padding-left: 1rem;
    padding-right: 1rem;
    padding-top: 0.5rem;
    padding-bottom: 0.5rem;
    max-width: 90%;
    border-radius: var(--rounded-box, 1rem);
    min-height: 2.75rem;
    min-width: 2.75rem;
    --tw-bg-opacity: 1;
    background-color: var(--fallback-n, oklch(var(--n) / var(--tw-bg-opacity)));
    --tw-text-opacity: 1;
    color: var(--fallback-nc, oklch(var(--nc) / var(--tw-text-opacity)));
}

.roll-bubble .custom {
    color: rgb(132 204 22 / var(--tw-text-opacity, 1));
}

.roll-bubble .skill-check {
    color: rgb(14 165 233 / var(--tw-text-opacity, 1));
}

.roll-bubble .saving-throw {
    color: rgb(249 115 22 / var(--tw-text-opacity, 1));
}
</style>
