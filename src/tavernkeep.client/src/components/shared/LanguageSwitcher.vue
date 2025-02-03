<script setup lang="ts">
import { useI18n } from '@/i18n/useI18n.ts';

const { t, locale, availableLocales } = useI18n();
</script>

<template>
    <label class="form-control w-full px-4">
        <select v-model="locale" name="language-toggle" class="select select-bordered">
            <option
                v-for="supportedLocale in availableLocales"
                :key="`locale-${supportedLocale}`"
                :value="supportedLocale"
            >
                {{ t(`locale.${supportedLocale}`) }}
            </option>
        </select>
    </label>
</template>

<style scoped></style>
