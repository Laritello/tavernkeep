import { ref, markRaw, shallowRef } from 'vue';
import type { AllowedComponentProps, Component, VNodeProps } from 'vue';

export type DialogResultCallback<T> = (result: DialogResult<T>) => void;
export type PropsType = { [key: string]: unknown; closeModal: DialogResultCallback<any> };
export type ComponentProps<C extends Component> = C extends new (...args: any) => any
    ? Omit<InstanceType<C>['$props'], keyof VNodeProps | keyof AllowedComponentProps | 'closeModal'>
    : never;

export type DialogConfirm = { action: 'confirm' };
export type DialogReject = { action: 'reject' };
export type DialogValue<T> = { action: 'result'; payload: T };
export type DialogResult<T> = DialogConfirm | DialogReject | DialogValue<T>;

const component = shallowRef<Component>();
const props = ref<PropsType>();
const isOpen = ref(false);

export function useModal() {
    function show<R, C extends Component>(
        modalComponent: C,
        componentProps?: ComponentProps<C>
    ): Promise<DialogResult<R>> {
        return new Promise((resolve) => {
            component.value = markRaw(modalComponent);
            props.value = {
                ...componentProps,
                closeModal: (result: DialogResult<R>) => {
                    resolve(result);
                    isOpen.value = false;
                },
            };
            isOpen.value = true;
        });
    }

    function showWithResult<R, C extends Component>(
        modalComponent: Component,
        resultType: R,
        componentProps?: ComponentProps<C>
    ): Promise<DialogResult<R>> {
        return show<typeof resultType, typeof modalComponent>(modalComponent, componentProps);
    }

    return {
        component,
        props,
        isOpen,
        show,
        showWithResult,
    };
}
