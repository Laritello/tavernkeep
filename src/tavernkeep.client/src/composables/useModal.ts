import { ref, markRaw, shallowRef } from 'vue';

type Payload<TResult = undefined> = TResult extends undefined
    ? { action: 'confirm' | 'reject' }
    : { action: 'result'; payload: TResult };

type DialogComponent = {
    new (...args: unknown[]): {
        $props: {
            [key: string]: unknown;
            closeModal: (result: Payload) => void;
        };
    };
};

type ModalProps<T extends DialogComponent> = InstanceType<T>['$props'];
type DialogResult<T extends DialogComponent> = ModalProps<T>['closeModal'] extends (result: infer R) => void
    ? R
    : unknown;

export type DialogResultCallback<TResult = undefined> = TResult extends undefined
    ? (payload: Payload) => void
    : (payload: Payload | Payload<TResult>) => void;

const component = shallowRef<DialogComponent>();
const props = ref();
const isOpen = ref(false);

export function useModal() {
    function show<C extends DialogComponent>(modalComponent: C, componentProps?: Omit<ModalProps<C>, 'closeModal'>) {
        return new Promise<DialogResult<C>>((resolve) => {
            component.value = markRaw(modalComponent);
            props.value = {
                ...componentProps,
                closeModal: (result: DialogResult<C>) => {
                    resolve(result);
                    isOpen.value = false;
                },
            };
            isOpen.value = true;
        });
    }

    return {
        component,
        props,
        isOpen,
        show,
        // showWithResult,
    };
}
