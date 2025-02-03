<template>
    <h1 class="mb-2">{{ t('builder.stages.welcome.contentHeader') }}</h1>
    <p>{{ t('builder.stages.welcome.contentMain') }}</p>
</template>

<script setup lang="ts">
import { useI18n } from 'vue-i18n';

import type { Character } from '@/entities';

defineProps<{
    character: Character;
}>();

defineEmits(['update']);

const { t } = useI18n();
</script>
