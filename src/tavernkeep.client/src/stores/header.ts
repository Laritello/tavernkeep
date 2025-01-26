import { defineStore } from 'pinia';

export const useHeaderStore = defineStore('header', {
  state: () => ({
    title: '' as string,
    subtitle: undefined as string | undefined,
  }),
  actions: {
    setHeader(title: string, subtitle?: string | undefined) {
      this.title = title;
      this.subtitle = subtitle;
    },
  },
});