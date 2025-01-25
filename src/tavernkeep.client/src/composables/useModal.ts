import { ref, markRaw, shallowRef, type VNodeProps, type AllowedComponentProps, type Component } from 'vue';

type Payload<TResult = undefined> = TResult extends undefined
    ? { action: 'confirm' | 'reject' }
    : { action: 'result'; payload: TResult };

type ModalProps<C extends Component> = C extends new (...args: unknown[]) => unknown
    ? Omit<InstanceType<C>['$props'], keyof VNodeProps | keyof AllowedComponentProps>
    : never;

type DialogResult<T extends Component> = ModalProps<T>['closeModal'] extends (result: infer R) => void ? R : unknown;

export type DialogResultCallback<TResult = undefined> = TResult extends undefined
    ? (payload: Payload) => void
    : (payload: Payload | Payload<TResult>) => void;

const component = shallowRef<Component>();
const props = ref();
const isOpen = ref(false);

export function useModal() {
    function show<C extends Component>(modalComponent: C, componentProps?: Omit<ModalProps<C>, 'closeModal'>) {
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
    };
}
