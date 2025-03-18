<script setup lang="ts">
defineProps<{
    statblock: string;
}>();
</script>

<template>
    <div v-html="statblock"></div>
</template>

<style scoped>
:root {
    --blue: #007bff;
    --indigo: #6610f2;
    --purple: #6f42c1;
    --pink: #e83e8c;
    --red: #dc3545;
    --orange: #fd7e14;
    --yellow: #ffc107;
    --green: #28a745;
    --teal: #20c997;
    --cyan: #17a2b8;
    --white: #fff;
    --gray: #6c757d;
    --gray-dark: #343a40;
    --primary: #6d8cc2;
    --secondary: #9f88b1;
    --success: #33b998;
    --info: #446691;
    --warning: #ffa000;
    --danger: #e22950;
    --light: #fcfaf7;
    --dark: #2c2831;
}

@font-face {
    font-family: 'ff-good-web-pro';
    src: url('/assets/fonts/ff-good-pro-bold.woff2') format('woff2');
    font-display: auto;
    font-style: normal;
    font-weight: 700;
}

@font-face {
    font-family: 'ff-good-web-pro';
    src: url('/assets/fonts/ff-good-pro-bold-italic.woff2') format('woff2');
    font-display: auto;
    font-style: italic;
    font-weight: 700;
}

@font-face {
    font-family: 'ff-good-web-pro';
    src: url('/assets/fonts/ff-good-pro-regular.woff2') format('woff2');
    font-display: auto;
    font-style: normal;
    font-weight: 400;
}

@font-face {
    font-family: 'ff-good-web-pro';
    src: url('/assets/fonts/ff-good-pro-regular-italic.woff2') format('woff2');
    font-display: auto;
    font-style: italic;
    font-weight: 400;
}

@font-face {
    font-family: 'ff-good-web-pro-condensed';
    src: url('/assets/fonts/ff-good-pro-condensed-bold.woff2') format('woff2');
    font-display: auto;
    font-style: normal;
    font-weight: 700;
}

@font-face {
    font-family: 'ff-good-web-pro-condensed';
    src: url('/assets/fonts/ff-good-pro-condensed-bold-italic.woff2') format('woff2');
    font-display: auto;
    font-style: italic;
    font-weight: 700;
}

@font-face {
    font-family: 'ff-good-web-pro-condensed';
    src: url('/assets/fonts/ff-good-pro-condensed-regular.woff2') format('woff2');
    font-display: auto;
    font-style: normal;
    font-weight: 400;
}

@font-face {
    font-family: 'ff-good-web-pro-condensed';
    src: url('/assets/fonts/ff-good-pro-condensed-regular-italic.woff2') format('woff2');
    font-display: auto;
    font-style: italic;
    font-weight: 400;
}

@-ms-viewport {
    width: device-width;
}

:deep(html) {
    font-size: 15px;
}

:deep(body) {
    font-family: 'Open Sans Condensed', sans-serif;
    font-weight: 400;
    background: transparent !important;
}

:deep(h1),
:deep(h2),
:deep(h3),
:deep(h4),
:deep(h5),
:deep(h6) {
    margin: 0;
    font-family: 'ff-good-web-pro-condensed', 'Oswald', sans-serif;
    padding: 0;
    line-height: 1;
    text-transform: uppercase;
    font-weight: 700;
}

:deep(h1) {
    font-size: 1.75rem;
}

:deep(h2) {
    font-size: 1.25rem;
}

:deep(h3) {
    font-size: 1.2rem;
}

:deep(h4) {
    font-size: 1.15rem;
}

:deep(h5) {
    font-size: 1.1rem;
}

:deep(h6) {
    font-size: 1.05rem;
}

:deep(.pf-trait) {
    display: inline-block;
    font-family: 'ff-good-web-pro-condensed', sans-serif;
    font-size: 0.9rem;
    line-height: 1;
    text-transform: uppercase;
    background: #5d0000;
    padding-left: 0.5rem !important;
    padding-right: 0.5rem;
    padding-top: 0.4rem;
    padding-bottom: 0.25rem;
    color: #fff;
    /* text-shadow: 0 0 0 #fff; */
    font-weight: bold;
    margin: 0;
    border: 2px solid #d8c384;
    /* min-width: 5rem; */
    text-align: center;
    letter-spacing: 0.05rem;
}

:deep(.pf-trait-edge) {
    padding: 0.4rem 0 0.25rem 0 !important;
    width: 3px;
    min-width: 0;
    overflow: hidden;
    vertical-align: top;
    background-color: #d8c384;
}

:deep(.pf-traits) {
    line-height: 1;
    display: inline-block;
    background: #d8c384;
    margin: 0;
    /* padding: 0 0.125rem; */
}

:deep(.pf-trait-size) {
    background: #3a7a58;
    min-width: auto;
}

:deep(.pf-trait-type) {
    background: #004316;
    min-width: auto;
}

:deep(.pf-trait-align) {
    background: #566193;
    min-width: auto;
}

:deep(.pf-trait-rare) {
    background: #002564;
    min-width: auto;
}

:deep(.pf-trait-uncommon) {
    background: #98503c;
    min-width: auto;
}

:deep(.pf-trait-unique) {
    background: #54166d;
    min-width: auto;
}

:deep(.bg-pf) {
    background-color: #f3f2ee;
}

:deep(.text-pf) {
    line-height: 140%;
    text-align: justify;
    font-size: 1.1rem;
    font-family: 'ff-good-web-pro';
}

:deep(.text-pre) {
    white-space: pre-wrap;
}

:deep(.text-img) {
    height: 1em;
    display: inline;
    vertical-align: baseline;
    width: auto;
    transform: translateY(0.15em);
}

:deep(.pf-action-free) {
    content: var(--pf-action-free);
}

:deep(.pf-action-reaction) {
    content: var(--pf-action-reaction);
}

:deep(.pf-action-one) {
    content: var(--pf-action-one);
}

:deep(.pf-action-two) {
    content: var(--pf-action-two);
}

:deep(.pf-action-three) {
    content: var(--pf-action-three);
}

:deep(.hang) {
    padding-left: 1em;
    text-indent: -1em;
}

:deep(.hang-nested) {
    padding-left: 2em;
    text-indent: -1em;
}

:deep(.pad) {
    padding-left: 1em;
}

:deep(.indent) {
    text-indent: 1em;
}

:deep(.divider) {
    background: rgb(119, 119, 119);
    height: 2px;
    margin: 0px 0px 0.2rem;
}

:deep(ul) {
    margin: 0;
    padding-left: 2rem;

    display: block;
    list-style-type: disc;
    unicode-bidi: isolate;
}

:deep(::marker) {
    unicode-bidi: isolate;
    font-variant-numeric: tabular-nums;
    text-transform: none;
    text-indent: 0px !important;
    text-align: start !important;
    text-align-last: start !important;
}

:deep(.positive){
    color: green;
}

:deep(.negative){
    color: red;
}
</style>
