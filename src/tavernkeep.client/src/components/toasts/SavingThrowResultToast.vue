<script setup lang="ts">
import type { SavingThrowRollMessage } from '@/entities';
import { useI18n } from '@/i18n/useI18n.ts';

const { t } = useI18n();
const { message } = defineProps<{ message: SavingThrowRollMessage }>();
</script>

<template>
    <div class="flex flex-row items-center justify-center gap-4">
        <div class="flex flex-col">
            <span class="text-xl font-bold">{{ t('chat.rollMessages.headers.savingthrow') }}</span>
            <span>
                {{ t(`pf.savingThrows.${message.savingThrow.name.toLowerCase()}`) }}
            </span>
        </div>
        <div class="flex flex-col grow">
            <span class="self-end text-4xl font-bold">{{ message.result.value }}</span>
        </div>
    </div>
</template>
