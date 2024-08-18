<template>
    <div v-for="condition in conditions" :key="condition.name" class="flex items-center">
        <span>{{ condition.name }}</span>
        <div v-if="condition.hasLevels" class="flex items-center">
            <div
                class="btn mx-2"
                :class="{ 'btn-disabled': condition.level <= 1 }"
                @click="decreaseCondtionLevel(condition)"
            >
                -
            </div>
            <span>{{ condition.level }}</span>
            <div class="btn mx-2" @click="increaseCondtionLevel(condition)">+</div>
        </div>
        <div class="btn mx-2" @click="removeCondtion(condition)">X</div>
    </div>
    <div class="btn" @click="showApplyDialog">Add</div>
</template>

<script lang="ts" setup>
import { useModal } from '@/composables/useModal';
import type { Condition } from '@/entities';
import ConditionApplyDialog from '../dialogs/ConditionApplyDialog.vue';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
const { characterId, conditions } = defineProps<{
    characterId: string;
    conditions: Condition[];
}>();

const modal = useModal();
const client = ApiClientFactory.createApiClient();

async function showApplyDialog() {
    const conditions = await client.getConditions();
    const result = await modal.show(ConditionApplyDialog, { conditions });
    if (result.action === 'result') {
        console.log(result.payload);
        await client.applyCondition(characterId, result.payload.name, result.payload.level);
    }
}

async function removeCondtion(condition: Condition) {
    await client.removeCondition(characterId, condition.name);
}

async function increaseCondtionLevel(condition: Condition) {
    await client.applyCondition(characterId, condition.name, condition.level + 1);
    condition.level += 1;
}

async function decreaseCondtionLevel(condition: Condition) {
    await client.applyCondition(characterId, condition.name, condition.level - 1);
    condition.level -= 1;
}
</script>

<style></style>
