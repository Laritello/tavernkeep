<script setup lang="ts">
import { onMounted, ref, watch } from 'vue';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();

interface Suggestion {
    name: string;
    description: string;
}

const { commands } = defineProps<{
    commands: Suggestion[];
}>();

const suggestions = ref<Suggestion[]>([]);
const showSuggestions = ref<boolean>(false);
const editableSpan = ref<HTMLInputElement | null>();
const content = defineModel<string>('content', {
    default: '',
});

// Process the content on input
const processInput = (event: Event): void => {
    const target = event.target as HTMLSpanElement;
    const message = sanitize(target.innerText);

    content.value = message; // Update the content state with the text inside the span

    updateSuggestions(message);
};

// Handle Enter key to prevent default behavior
const handleEnter = (event: KeyboardEvent): void => {
    if (event.key === 'Enter') {
        event.preventDefault();
        // Optionally, trigger an action (e.g., submit)
    }
};

// Watch for changes to `content` and update the span's innerText
watch(content, (newValue) => {
    if (editableSpan.value && editableSpan.value.innerText !== newValue) {
        editableSpan.value.innerText = newValue;
    }
});

// Ensure the span is initialized with the correct content
onMounted(() => {
    if (editableSpan.value) {
        editableSpan.value.innerText = content.value;
    }
});

// Span doesn't want to remove <br> when message is deleted completele, so we force it.
function sanitize(text: string): string {
    if (text.trim() === '') {
        return '';
    }
    return text;
}

function updateSuggestions(message: string) {
    if (!message.startsWith('/')) {
        showSuggestions.value = false;
        return;
    }
    suggestions.value = commands.filter((command) => command.name.startsWith(message));
    showSuggestions.value = suggestions.value.length > 0;
}

function selectSuggestion(suggestion: Suggestion) {
    content.value = suggestion.name + ' ';
    suggestions.value = [];
    showSuggestions.value = false;
    editableSpan.value?.focus();
}

function tryToCompleteSuggestion() {
    if (!showSuggestions.value) return;
    selectSuggestion(suggestions.value[0]);
}
</script>

<template>
    <div class="dropdown dropdown-top dropdown-end form-control py-1 pr-1 m-1">
        <!--Screw firefox for now, they should get plaintext-only support soon (v136)-->
        <!--
        TODO: After entering the command from the suggestion menu caret doesn't move to the last symbol.
        -->
        <span class="w-full border rounded-3xl p-2 px-3 pt-3 min-h-12 max-h-24 overflow-y-auto shadow bg-base-100"
            :placeholder='t("chat.input.placeholder")' contenteditable="plaintext-only" type="text" tabindex="0"
            ref="editableSpan" @input="processInput" @keydown.enter="handleEnter"
            @keydown.tab.prevent="tryToCompleteSuggestion" />

        <ul v-show="showSuggestions" tabindex="0"
            class="menu dropdown-content bg-base-100 rounded-box z-[1] mt-4 w-full border p-2 shadow">
            <li v-for="suggestion in suggestions" :key="suggestion.name" @click="selectSuggestion(suggestion)"
                class="p-1">
                <div class="flex flex-col items-start">
                    <h1 class="text-lg">{{ suggestion.name }}</h1>
                    <p class="text-sm font-thin text-slate-400">{{ suggestion.description }}</p>
                </div>
            </li>
        </ul>
    </div>
</template>

<style scoped>
span {
    white-space: pre-wrap;
}

span:empty:before {
    content: attr(placeholder);
    color: #aaa;
}
</style>