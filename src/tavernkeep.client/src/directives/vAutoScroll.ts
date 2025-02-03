import type { DirectiveBinding } from 'vue';

interface InternalHTMLElement extends HTMLElement {
    _autoScrollObserver?: MutationObserver;
    _autoScrollRafId?: number;
}

export type AutoScrollOptions = {
    behavior: ScrollBehavior;
    threshold: number;
    initialScroll: boolean;
};

const defaultConfig: AutoScrollOptions = {
    behavior: 'smooth',
    threshold: 50,
    initialScroll: true,
};

export const vAutoScroll = {
    mounted(el: InternalHTMLElement, binding: DirectiveBinding<Partial<AutoScrollOptions>>) {
        const options: AutoScrollOptions = {
            ...defaultConfig,
            ...(binding.value || {}),
        };

        const shouldScroll = () => {
            const { scrollTop, clientHeight, scrollHeight } = el;

            if (clientHeight === scrollHeight) return false;

            return scrollHeight - (scrollTop + clientHeight) <= options.threshold;
        };

        const scrollToBottom = (force = false) => {
            if (force || shouldScroll()) {
                if (el._autoScrollRafId) {
                    cancelAnimationFrame(el._autoScrollRafId);
                }

                el._autoScrollRafId = requestAnimationFrame(() => {
                    const behavior = force ? 'auto' : options.behavior;

                    el.scrollTo({
                        top: el.scrollHeight,
                        behavior,
                    });
                });
            }
        };

        let isInitialScroll = true;
        const observer = new MutationObserver(() => {
            if (isInitialScroll) {
                scrollToBottom(true);
                isInitialScroll = false;
                return;
            }

            scrollToBottom();
        });

        observer.observe(el, { childList: true, subtree: true });
        el._autoScrollObserver = observer;

        if (options.initialScroll) {
            scrollToBottom(true);
        }
    },

    unmounted(el: InternalHTMLElement) {
        if (el._autoScrollObserver) {
            el._autoScrollObserver.disconnect();
        }

        if (el._autoScrollRafId) {
            cancelAnimationFrame(el._autoScrollRafId);
        }
    },
};
