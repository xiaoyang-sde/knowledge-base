module.exports = {
  env: {
    browser: true,
    es2021: true,
    webextension: true,
  },
  extends: [
    'airbnb-base',
  ],
  parserOptions: {
    ecmaVersion: 12,
    sourceType: 'script',
    "ecmaFeatures": {
      "impliedStrict": true,
    }
  },
  rules: {
  },
};
