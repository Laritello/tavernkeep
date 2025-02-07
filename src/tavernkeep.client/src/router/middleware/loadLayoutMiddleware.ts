import type { RouteLocation } from 'vue-router';

import Layouts from '@/layouts';

export function loadLayoutMiddleware(to: RouteLocation) {
    const layout = to.meta.layout || 'BlankLayout';
    if (layout in Layouts) {
        to.meta.layoutComponent = Layouts[layout];
    } else {
        console.warn('Unknown layout', layout);
        to.meta.layoutComponent = Layouts['BlankLayout'];
    }
}
