<template>
    <Teleport to="#modal">
        <component
            v-if="modal.isOpen.value"
            :is="modal.component.value"
            v-bind="{ ...modal.props.value }"
            :class="{ 'modal-open': modal.isOpen.value }"
        />
    </Teleport>
</template>
<script setup lang="ts">
import { onBeforeUnmount, onMounted } from 'vue';
import { useModal } from '@/composables/useModal';

const modal = useModal();

const buttonPressHandler = (e: KeyboardEvent) => {
    if (!modal.isOpen.value || !modal.props.value) {
        return;
    }

    if (e.key === 'Escape') {
        modal.props.value.closeModal({ action: 'reject' });
    }
};

onMounted(() => {
    document.addEventListener('keydown', buttonPressHandler);
});

onBeforeUnmount(() => {
    document.removeEventListener('keydown', buttonPressHandler);
});
</script>
