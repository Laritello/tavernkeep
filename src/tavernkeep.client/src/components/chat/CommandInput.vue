<template>
    <div>
        <div class="dropdown dropdown-top dropdown-end w-full form-control">
            <textarea type="text" v-model="query" tabindex="0" ref="textInput" @input="updateSuggestions"
                @keydown.tab.prevent="tryToCompleteSuggestion" class="textarea w-full border rounded-xl" 
                placeholder="Type here..."></textarea>
            <ul v-show="showSuggestions" tabindex="0"
                class="absolute dropdown-content z-[1] menu p-2 shadow bg-base-100 rounded w-full">
                <li v-for="suggestion in suggestions" :key="suggestion.name" @click="selectSuggestion(suggestion)"
                    class="p-2">
                    <div class="flex flex-col items-start">
                        <h1 class="text-xl">{{ suggestion.name }}</h1>
                        <p class="text-sm font-thin text-slate-400">{{ suggestion.description }}</p>
                    </div>
                </li>
            </ul>
        </div>
    </div>
</template>
<script setup lang="ts">
import { ref } from 'vue';

interface Suggestion {
    name: string;
    description: string;
}

const query = defineModel<string>();
const { commands } = defineProps<{
    commands: Suggestion[];
}>();

const suggestions = ref<Suggestion[]>([]);
const textInput = ref<HTMLInputElement>();
const showSuggestions = ref(false);

function updateSuggestions() {
    if (!query.value?.startsWith('/')) {
        showSuggestions.value = false;
        return;
    }
    suggestions.value = commands.filter((command) => command.name.startsWith(query.value!));
    showSuggestions.value = suggestions.value.length > 0;
}

function selectSuggestion(suggestion: Suggestion) {
    query.value = suggestion.name + ' ';
    suggestions.value = [];
    showSuggestions.value = false;
    textInput.value?.focus();
}

function tryToCompleteSuggestion() {
    if (!showSuggestions.value) return;
    selectSuggestion(suggestions.value[0]);
}
</script>
