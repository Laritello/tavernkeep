<template>
    <div class="card card-body bg-neutral text-neutral-content">
        <h1 class="card-title">Conditions</h1>
        <div v-for="condition in conditions" :key="condition.name" class="grid grid-cols-3 grid-flow-col">
            <span class="col-start-1">{{ condition.name }}</span>
            <div v-if="condition.hasLevels" class="flow col-start-2 mx-auto">
                <div
                    class="btn btn-xs mx-2"
                    :class="{ 'btn-disabled': condition.level <= 1 }"
                    @click="$emit('decreaseConditionLevel', condition)"
                >
                    -
                </div>
                <span>{{ condition.level }}</span>
                <div class="btn btn-xs mx-2" @click="$emit('increaseConditionLevel', condition)">+</div>
            </div>
            <div class="mx-auto">
                <button class="btn btn-error btn-xs mx-2 col-start-3 w-8" @click="$emit('removeCondition', condition)">
                    X
                </button>
            </div>
        </div>
        <button class="btn btn-success btn-sm" @click="$emit('addCondition')">Add</button>
    </div>
</template>

<script setup lang="ts">
import type { Condition } from '@/entities';
const { conditions } = defineProps<{
    conditions: Condition[];
}>();
</script>
<style>
.align-right {
    display: flex;
    justify-content: right;
    align-items: flex-end;
}
</style>
