<script setup lang="ts"></script>

<template>
    <Component :is="$route.meta.layoutComponent">
        <RouterView />
    </Component>
</template>
