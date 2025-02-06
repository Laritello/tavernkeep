<script setup lang="ts">
import { SkillType } from '@/contracts/enums';
import type { SkillRollMessage } from '@/entities';
import { useI18n } from '@/i18n/useI18n.ts';

const { t } = useI18n();
const { message } = defineProps<{ message: SkillRollMessage }>();
</script>

<template>
    <div class="flex flex-row items-center justify-center gap-4">
        <div class="flex flex-col">
            <span class="text-xl font-bold">{{ t('chat.rollMessages.headers.skill') }}</span>
            <span>
                {{
                    message.skill.type === SkillType.Basic
                        ? t(`pf.skills.${message.skill.name.toLowerCase()}`)
                        : message.skill.name
                }}
            </span>
        </div>
        <div class="flex flex-col grow">
            <span class="self-end text-4xl font-bold">{{ message.result.value }}</span>
        </div>
    </div>
</template>
