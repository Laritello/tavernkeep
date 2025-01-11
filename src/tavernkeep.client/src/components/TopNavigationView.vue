<script setup lang="ts">
import HealthBar from '@/components/character/HealthBar.vue';

import { ApiClientFactory } from '@/factories/ApiClientFactory';
import type { AxiosApiClient } from '@/api/axios/AxiosApiClient';

import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';
import ArmorClassWidget from '@/components/character/ArmorClassWidget.vue';
import PerceptionWidget from '@/components/character/PerceptionWidget.vue';
import HeroPoints from './character/HeroPoints.vue';
import { useModal } from '@/composables/useModal';
import ConditionApplyDialog from '@/components/dialogs/ConditionApplyDialog.vue';
import { useI18n } from 'vue-i18n';
import { ref } from 'vue';

const { t } = useI18n();

const api: AxiosApiClient = ApiClientFactory.createApiClient();

const user = useCurrentUserAccount();
const modal = useModal();

async function showConditionEditDialog() {
    if (user.activeCharacter.value !== undefined) {
        const result = await modal.show(ConditionApplyDialog, {
            conditions: await api.getConditions(),
            active: user.activeCharacter.value.conditions
        });

        if (result.action === 'result') {
            await api.editConditions(user.activeCharacter.value.id, result.payload);
        }
    }
}

async function performLongRest() {
    if (user.activeCharacter.value !== undefined) {
        await api.performLongRest(user.activeCharacter.value.id);
    }
}

const collapsed = ref(true);

async function toggleDetails() {
    collapsed.value = !collapsed.value;
}
</script>

<template>
    <div class="navbar bg-base-100 z-10 min-h-fit border-b-2 border-base-300 py-0">
        <div class="flex flex-col w-full items-stretch">
            <!-- <div v-if="user.activeCharacter.value !== undefined">
                <p class="text-sm text-center leading-4 font-semibold">{{ user.activeCharacter.value.name }}</p>
                <p class="text-xs text-center leading-3">{{ user.activeCharacter.value.ancestry }} {{
                    user.activeCharacter.value.class }} {{ user.activeCharacter.value.level }}</p>
            </div> -->
            <div class="flex flex-row mt-1">
                <div class="flex-none w-24">
                    <div class="flex flex-col gap-1">
                        <div class="flex flex-row">
                            <!--Settings button-->
                            <div class="w-12">
                                <div class="flex items-center justify-center relative">
                                    <svg class="w=full h-full" viewBox="0 -960 960 960"
                                        xmlns="http://www.w3.org/2000/svg" fill="currentColor">
                                        <path
                                            d="m479.22-880a400 400 0 0 0-399.22 400 400 400 0 0 0 400 400 400 400 0 0 0 400-400 400 400 0 0 0-400-400 400 400 0 0 0-0.78124 0zm0.78124 28.672a371.3 371.3 0 0 1 371.33 371.33 371.3 371.3 0 0 1-371.33 371.33 371.3 371.3 0 0 1-371.33-371.33 371.3 371.3 0 0 1 371.33-371.33z" />
                                        <path
                                            d="m408.86-221.29-10.348-82.786q-8.408-3.2338-15.846-7.7612-7.4378-4.5274-14.552-9.7015l-76.965 32.338-71.144-122.89 66.617-50.448q-0.64676-4.5274-0.64676-8.7313v-17.463q0-4.204 0.64676-8.7314l-66.617-50.448 71.144-122.89 76.965 32.338q7.1144-5.1741 14.876-9.7015 7.7612-4.5274 15.522-7.7612l10.348-82.786h142.29l10.348 82.786q8.408 3.2338 15.846 7.7612 7.4378 4.5274 14.552 9.7015l76.965-32.338 71.144 122.89-66.617 50.448q0.64676 4.5274 0.64676 8.7314v17.463q0 4.204-1.2935 8.7313l66.617 50.448-71.144 122.89-76.318-32.338q-7.1144 5.1741-14.876 9.7015-7.7612 4.5274-15.522 7.7612l-10.348 82.786zm45.274-51.741h51.095l9.0547-68.557q20.05-5.1741 37.189-15.199 17.139-10.025 31.368-24.254l64.03 26.517 25.224-43.98-55.622-42.04q3.2338-9.0547 4.5274-19.08 1.2935-10.025 1.2935-20.373 0-10.348-1.2935-20.373-1.2935-10.025-4.5274-19.08l55.622-42.04-25.224-43.98-64.03 27.164q-14.229-14.876-31.368-24.9-17.139-10.025-37.189-15.199l-8.408-68.557h-51.095l-9.0547 68.557q-20.05 5.1741-37.189 15.199-17.139 10.025-31.368 24.254l-64.03-26.517-25.224 43.98 55.622 41.393q-3.2338 9.7015-4.5274 19.403-1.2935 9.7015-1.2935 20.697 0 10.348 1.2935 20.05 1.2935 9.7015 4.5274 19.403l-55.622 42.04 25.224 43.98 64.03-27.164q14.229 14.876 31.368 24.9 17.139 10.025 37.189 15.199zm27.164-116.42q37.512 0 64.03-26.517 26.517-26.517 26.517-64.03t-26.517-64.03q-26.517-26.517-64.03-26.517-38.159 0-64.353 26.517-26.194 26.517-26.194 64.03t26.194 64.03 64.353 26.517z" />
                                    </svg>
                                </div>
                            </div>

                            <!--Long rest button-->
                            <div class="w-12">
                                <div class="flex items-center justify-center relative" @click="performLongRest">
                                    <svg class="w=full h-full" viewBox="0 -960 960 960"
                                        xmlns="http://www.w3.org/2000/svg" fill="currentColor">
                                        <path
                                            d="m479.22-880a400 400 0 0 0-399.22 400 400 400 0 0 0 400 400 400 400 0 0 0 400-400 400 400 0 0 0-400-400 400 400 0 0 0-0.78124 0zm0.78124 28.672a371.3 371.3 0 0 1 371.33 371.33 371.3 371.3 0 0 1-371.33 371.33 371.3 371.3 0 0 1-371.33-371.33 371.3 371.3 0 0 1 371.33-371.33z" />
                                        <path
                                            d="m312.68-232.11c-42.279-20.166-41.982-77.445 0.5048-97.416l12.146-5.7093-10.156-5.1091c-21.34-10.736-34.303-29.337-34.303-49.223 0-10.749 7.3986-29.805 14.712-37.892 7.7821-8.6057 24.068-15.172 38.133-15.376 7.3905-0.1068 13.437-0.3797 13.437-0.6063 0-0.2267-2.8125-6.5838-6.25-14.127-18.095-39.708-14.374-103.77 8.9544-154.16 15.621-33.743 49.721-73.112 84.718-97.807 20.962-14.792 64.931-36.286 77.742-38.004 16.409-2.2009 19.699 3.0877 17.677 28.414-1.7709 22.179 1.8481 41.815 10.984 59.595 2.6842 5.224 19.08 23.862 36.436 41.419 33.395 33.782 47.346 54.85 54.763 82.706 6.1745 23.188 3.0229 57.272-7.4947 81.052l-4.0969 9.2629 15.299 3.0099c28.185 5.5452 44.579 26.806 42.755 55.449-1.3471 21.156-10.66 35.022-30.022 44.696l-13.734 6.8624 9.9913 4.1737c21.175 8.8455 34.251 28.178 34.251 50.642 0 27.592-19.239 49.272-46.105 51.953-12.954 1.2929-18.651-0.3862-83.267-24.542l-69.362-25.93-65.981 24.819c-72.386 27.228-84.852 29.906-101.73 21.854zm85.264-49.182c254.85-94.226 250.54-92.337 250.54-109.81 0-9.9415-10.08-20.171-19.877-20.171-4.0377 0-24.487 6.4084-45.443 14.241-20.956 7.8324-82.344 30.693-136.42 50.802-54.073 20.109-104.99 39.09-113.15 42.181-11.83 4.4802-15.681 7.384-18.987 14.316-3.8424 8.0576-3.8311 9.3573 0.1543 17.715 3.7686 7.9029 10.814 13.285 18.068 13.802 1.2488 0.089 30.552-10.295 65.118-23.075zm243.07 15.772c12.138-12.138 8.4224-30.189-7.4766-36.328-3.7206-1.4368-15.53-5.8291-26.242-9.7607l-19.478-7.1485-26.488 9.9443c-14.568 5.4694-28.211 10.586-30.318 11.37-3.4049 1.2677-3.4049 1.6602 0 3.5325 3.8895 2.1389 63.266 24.449 84.27 31.664 15.769 5.4165 17.228 5.2308 25.733-3.2735zm-238.43-97.762c15.858-5.9083 28.832-11.477 28.832-12.376s-11.477-5.8907-25.505-11.094c-73.09-27.111-77.7-28.204-85.61-20.295-9.2545 9.2544-10.035 24.906-1.6153 32.407 3.701 3.2976 46.243 20.584 53.893 21.898 0.6453 0.1108 14.148-4.6324 30.005-10.541zm103.03-44.211c16.543-5.8368 26.793-14.956 34.487-30.682 6.3239-12.925 6.8165-16.024 5.5872-35.134-0.7415-11.527-3.3891-26.129-5.8835-32.45-5.1722-13.105-25.573-45.966-28.536-45.964-1.0817 7e-4 -2.0147 8.9065-2.0734 19.791-0.086 15.867-1.4485 22.698-6.8756 34.46-8.4552 18.326-14.629 21.707-31.81 17.422-13.518-3.3717-33.986-14.306-33.986-18.156 0-1.3453-1.5822-2.446-3.516-2.446-5.3692 0-14.359 23.564-14.359 37.638 0 15.516 10.015 36.611 21.563 45.419 17.72 13.516 44.152 17.598 65.402 10.1zm87.516-53.038c9.3847-18.707 12.842-44.743 8.5936-64.714-4.9155-23.108-17.678-42.357-48.011-72.412-14.9-14.764-30.634-31.807-34.963-37.874-9.2212-12.921-17.404-36.573-19.816-57.28-0.976-8.3769-2.9727-14.683-4.649-14.683-3.9519 0-35.217 18.397-48.421 28.491-55.766 42.635-83.878 95.774-84.22 159.2-0.1575 29.255 4.1301 46.392 16.386 65.49 4.3747 6.8174 8.3486 12.395 8.8309 12.395s0.825-8.9057 0.7617-19.791c-0.1457-25.041 5.6786-40.978 23.582-64.528 20.062-26.39 34.032-29.224 40.76-8.2699 3.37 10.496 8.6761 17.594 16.138 21.587 6.9731 3.7319 9.1947-1.3169 9.2408-21.001 0.032-13.795-1.4781-19.837-8.756-35.023-4.8373-10.094-11.305-21.748-14.373-25.897-9.8488-13.321 0.1524-28.976 15.958-24.979 20.394 5.1577 70.094 49.181 85.816 76.014 16.858 28.772 25.669 67.402 20.909 91.673-1.2397 6.3203-2.3039 12.554-2.3651 13.852-0.2482 5.2677 12.362-9.8189 18.599-22.252z" />
                                    </svg>
                                </div>
                            </div>
                        </div>
                        <button v-if="user.activeCharacter.value !== undefined"
                            class="btn btn-xs btn-outline uppercase px-0" :class="{ 'rounded-b-none': !collapsed }"
                            v-on:click="showConditionEditDialog">
                            <div class="flex flex-row">
                                <p class="tracking-tighter">
                                    <span>{{ t('widgets.conditions.conditions') }}</span>
                                    <span v-if="user.activeCharacter.value.conditions.length > 0" class="ml-1">{{
                                        user.activeCharacter.value.conditions.length }}</span>
                                </p>
                            </div>
                        </button>
                    </div>
                </div>

                <div class="flex flex-col flex-1 h-full gap-1">
                    <div class="avatar self-center">
                        <div class="w-12 rounded-full border-2 border-primary">
                            <img alt="Character's avatar"
                                src="https://www.fantasyflightgames.com/media/ffg_content/dark-heresy/images/WH_Pushed-to-the-Limit_HRF_090918_IFS.jpg" />
                        </div>
                    </div>
                    <div class="self-stretch px-2" v-if="user.activeCharacter.value !== undefined">
                        <HealthBar @click="() => console.log('Health edit')" :health="user.activeCharacter.value.health"
                            class="h-6" />
                    </div>
                </div>

                <div v-if="user.activeCharacter.value !== undefined" class="flex-none w-24">
                    <div class="flex flex-col gap-1">
                        <div class="flex flex-row">
                            <PerceptionWidget :perception="user.activeCharacter.value.perception" class="w-12" />
                            <ArmorClassWidget :armor="user.activeCharacter.value.armor" class="w-12" />
                        </div>
                        <HeroPoints class="flex-none" />
                    </div>
                </div>
            </div>

            <div class="transition-all overflow-hidden  ease-in-out duration-300"
                :class="{ 'max-h-screen': !collapsed, 'max-h-0': collapsed }">
                <div class="flex flex-row pb-1">
                    <div class="flex flex-col border border-t-0 rounded-b-lg conditions-list w-24 mb-1">

                    </div>
                    <div v-if="user.activeCharacter.value !== undefined" class="flex flex-col flex-1 pt-1">
                        <p class="text-sm text-center leading-4 font-semibold">{{ user.activeCharacter.value.name }}</p>
                        <p class="text-xs text-center leading-3">{{ user.activeCharacter.value.ancestry }} {{
                            user.activeCharacter.value.class }} {{ user.activeCharacter.value.level }}</p>
                    </div>
                    <div class="flex-none w-24">
                        <div v-if="user.activeCharacter.value !== undefined" class="flex flex-row">
                            <PerceptionWidget :perception="user.activeCharacter.value.perception" class="w-12" />
                            <ArmorClassWidget :armor="user.activeCharacter.value.armor" class="w-12" />
                        </div>
                    </div>
                </div>
            </div>

            <button class="btn btn-xs btn-ghost uppercase" @click="toggleDetails">
                Details
            </button>
        </div>
    </div>
</template>

<style scoped>
.conditions-list {
    border-color: currentColor;
}
</style>
