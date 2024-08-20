<template>
    <div class="card card-body bg-neutral text-neutral-content">
        <h1 class="card-title">Armor class</h1>
        <div class="grid grid-cols-2 gap-4">
            <div class="col-start-1">Armor class</div>
            <span class="col-start-2 mx-auto">{{ armor.class }}</span>
            <!-- <div class="col-start-3">{{ armor.proficiency }}</div>
            <button class="col-start-4 btn btn-xs w-10 mx-auto" @click="$emit('edit', armor)">Edit</button> -->
        </div>
    </div>
</template>

<script setup lang="ts">
import type { ArmorClass } from '@/contracts/character';

defineProps<{
    armor: ArmorClass;
}>();
</script>
