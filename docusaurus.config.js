/** @type {import('@docusaurus/types').DocusaurusConfig} */
const math = require('remark-math');
const katex = require('rehype-katex');

module.exports = {
  title: 'Xiaoyang\'s Notes',
  tagline: 'Lecture notes on Computer Science, Web Development, and Software Engineering Philosophies.',
  url: 'https://xiaoyang-liu.com',
  baseUrl: '/',
  onBrokenLinks: 'throw',
  onBrokenMarkdownLinks: 'warn',
  favicon: 'img/favicon.png',
  organizationName: 'xiaoyang-sde',
  projectName: 'programming-note',
  stylesheets: [
    'https://fonts.googleapis.com/css2?family=Inter&display=swap',
    'https://cdn.jsdelivr.net/npm/katex@0.12.0/dist/katex.min.css',
  ],
  themeConfig: {
    algolia: {
      appId: 'VI4IHQNLIL',
      apiKey: 'af43094cb4b5d38f484b366cf389a142',
      indexName: 'xiaoyang-liu',
    },
    navbar: {
      title: 'Xiaoyang\'s Notes',
      logo: {
        alt: 'Site Logo',
        src: 'img/logo.png',
      },
      items: [
        {
          type: 'doc',
          docId: 'intro',
          position: 'left',
          label: 'Notes',
        },
        {
          href: 'https://github.com/xiaoyang-sde/',
          label: 'GitHub',
          position: 'left',
        },
      ],
    },
    footer: {
      style: 'light',
      copyright: `Copyright © ${new Date().getFullYear()} Xiaoyang Liu. Built with Docusaurus.`,
    },
  },
  presets: [
    [
      '@docusaurus/preset-classic',
      {
        docs: {
          sidebarPath: require.resolve('./sidebars.js'),
          remarkPlugins: [math],
          rehypePlugins: [katex],
          editUrl:
            'https://github.com/xiaoyang-sde/programming-note/edit/master/',
        },
        blog: {
          showReadingTime: true,
          editUrl:
            'https://github.com/facebook/docusaurus/edit/master/blog/',
        },
        theme: {
          customCss: require.resolve('./src/css/custom.css'),
        },
        googleAnalytics: {
          trackingID: 'UA-156941792-1',
          anonymizeIP: true,
        },
      },
    ],
  ],
};
