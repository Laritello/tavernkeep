<script setup lang="ts">
import { useFileDialog } from '@vueuse/core';
import { ref, useTemplateRef } from 'vue';
import { Cropper, CircleStencil } from 'vue-advanced-cropper';
import 'vue-advanced-cropper/dist/style.css';
import { useRoute, useRouter } from 'vue-router';

import { ApiClientFactory } from '@/factories/ApiClientFactory.ts';
import { useHeaderStore } from '@/stores/header.ts';

const api = ApiClientFactory.createApiClient();
const route = useRoute();
const router = useRouter();
const header = useHeaderStore();

header.setHeader('Character image');

const image = ref<string | null>(null);
const error = ref<string | null>(null);
const uploading = ref(false);
const cropperRef = useTemplateRef('cropper');

function openFile() {
    const { open, onChange } = useFileDialog({
        accept: 'image/*',
        multiple: false,
    });

    onChange((files) => {
        if (!files || files.length === 0) {
            return;
        }

        const fileReader = new FileReader();
        fileReader.onload = (e) => (image.value = e.target?.result as string);
        fileReader.readAsDataURL(files[0]);
        console.log(files[0]);
    });

    open();
}

function showError(message: string) {
    error.value = message;
    setTimeout(() => (error.value = null), 5000);
    uploading.value = false;
}

async function upload() {
    uploading.value = true;
    const { canvas } = cropperRef.value!.getResult();

    if (!canvas) {
        showError('Cropper canvas is not defined');
        return;
    }

    const blob = await new Promise<Blob | null>((resolve) => canvas.toBlob((blob) => resolve(blob), 'image/png'));

    if (!blob) {
        showError('Image blob is not defined');
        return;
    }

    const file = new File([blob], 'portrait.png', { type: 'image/png' });
    const characterId = route.params['characterId'] as string;

    await api.updatePortrait(characterId, file);
    await router.push('/settings');
}
</script>

<template>
    <div class="card bg-base-200 max-w-[1024px] my-4 shadow-xl">
        <figure class="w-full max-h-96 px-10 pt-10">
            <Cropper
                v-if="!!image"
                ref="cropper"
                :src="image"
                :stencil-component="CircleStencil"
                :stencil-size="{
                    width: 300,
                    height: 300,
                }"
                :stencil-props="{
                    handlers: {},
                    movable: false,
                    resizable: false,
                    aspectRatio: 1,
                }"
                image-restriction="stencil"
            />
            <div v-else class="text-center">
                <h1 class="text-xl">No image</h1>
                <span class="mdi mdi-image-off text-7xl"></span>
            </div>
        </figure>
        <div class="card-body items-center text-center">
            <div class="card-actions">
                <button v-if="!!image" class="btn btn-primary" :disabled="uploading" @click="upload">
                    <template v-if="!uploading">
                        <span class="mdi mdi-cloud-upload"></span>
                        Upload
                    </template>
                    <template v-else>
                        <span class="loading loading-spinner"></span>
                        uploading...
                    </template>
                </button>
                <button v-else class="btn btn-primary" @click="openFile">Open image</button>
            </div>
            <h1 class="text-error">{{ error }}</h1>
        </div>
    </div>
</template>

<style scoped></style>
