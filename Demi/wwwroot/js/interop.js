window.getCssVariable = function (variableName) {
    return getComputedStyle(document.documentElement).getPropertyValue(variableName);
};