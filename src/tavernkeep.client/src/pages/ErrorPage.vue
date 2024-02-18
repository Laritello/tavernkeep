<template>
    <h1>Error!</h1>
    <br />
    <p>{{ errorMessage }}</p>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router';
const route = useRoute();
const errorMessage = route.meta.errorMessage;
</script>
