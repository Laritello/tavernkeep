<template>
    <Teleport to="#modal">
        <Transition name="fade">
            <component
                v-if="modal.isOpen.value"
                :is="modal.component.value"
                v-bind="{ ...modal.props.value }"
                @click="onClick"
                class="modal-open"
            />
        </Transition>
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

const onClick = (e: MouseEvent) => {
    if (e.target === e.currentTarget) {
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

<style scoped>
.fade-enter-active,
.fade-leave-active {
    @apply transition ease-out duration-300;
}

.fade-leave-to,
.fade-enter-from {
    @apply opacity-0;
}

.fade-enter-to,
.fade-leave-from {
    @apply opacity-100;
}
</style>
