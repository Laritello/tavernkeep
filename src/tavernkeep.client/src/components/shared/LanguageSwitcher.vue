<script setup lang="ts">
import { useI18n } from 'vue-i18n';
import translation from '@/i18n/translation';

const { t, locale } = useI18n();
const supportedLocales = translation.supportedLocales;

const switchLanguage = async (event: any) => {
    const newLocale = event.target.value;
    await translation.switchLanguage(newLocale);
}

</script>

<template>
    <label class="form-control w-full px-4">
        <select name="language-toggle" class="select select-bordered" @change="switchLanguage">
            <option v-for="supportedLocale in supportedLocales" :key="`locale-${supportedLocale}`"
                :value="supportedLocale" :selected="locale === supportedLocale">
                {{ t(`locale.${supportedLocale}`) }}
            </option>
        </select>
    </label>
</template>

<style scoped></style>