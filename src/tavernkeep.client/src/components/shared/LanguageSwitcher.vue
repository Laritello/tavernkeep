<script setup lang="ts">
import { useI18n, type LocaleType } from '@/i18n/useI18n.ts';

const { t, locale, availableLocales, changeLocale } = useI18n();

const switchLanguage = async (event: Event) => {
    const target = event.target as HTMLSelectElement;
    const newLocale = target.value as LocaleType;
    changeLocale(newLocale);
};
</script>

<template>
    <label class="form-control w-full px-4">
        <select name="language-toggle" class="select select-bordered" @change="switchLanguage">
            <option
                v-for="supportedLocale in availableLocales"
                :key="`locale-${supportedLocale}`"
                :value="supportedLocale"
                :selected="locale === supportedLocale"
            >
                {{ t(`locale.${supportedLocale}`) }}
            </option>
        </select>
    </label>
</template>

<style scoped></style>
