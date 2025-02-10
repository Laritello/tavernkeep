<script setup lang="ts">
import { useFileDialog } from '@vueuse/core';
import { ref, useTemplateRef } from 'vue';
import { Cropper, CircleStencil } from 'vue-advanced-cropper';
import 'vue-advanced-cropper/dist/style.css';
import { useRouter } from 'vue-router';

import { useHeaderStore } from '@/stores/header.ts';

const header = useHeaderStore();
header.setHeader('Character image');

const uploading = ref(false);
const image = ref<string | null>(null);
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

async function upload() {
    const result = cropperRef.value?.getResult();
    if (!result) {
        return;
    }

    const uploadData = result.canvas?.toDataURL('png');
    console.log(uploadData);

    // TODO: upload file to server
    // TODO: handle loading progress
    uploading.value = true;

    const router = useRouter();
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
                <button v-if="!!image" class="btn btn-primary" @click="upload">
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
        </div>
    </div>
</template>

<style scoped></style>
