/** @type {import('@docusaurus/types').DocusaurusConfig} */
const math = require('remark-math');
const katex = require('rehype-katex');

const lightCodeTheme = require('prism-react-renderer/themes/github');
const darkCodeTheme = require('prism-react-renderer/themes/dracula');

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
    {
      href: 'https://cdn.jsdelivr.net/npm/katex@0.13.24/dist/katex.min.css',
      type: 'text/css',
      integrity: 'sha384-odtC+0UGzzFL/6PNoE8rX/SPcQDXBJ+uRepguP4QkPCm2LBxH3FA3y+fKSiJ+AmM',
      crossorigin: 'anonymous',
    },
  ],
  themeConfig: {
    algolia: {
      appId: 'VI4IHQNLIL',
      apiKey: '50eac323e30a25d563ef87b2dffbc928',
      indexName: 'xiaoyang-liu',
      contextualSearch: false,
      facetFilters: [],
    },
    prism: {
      theme: lightCodeTheme,
      darkTheme: darkCodeTheme,
      additionalLanguages: ['rust'],
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
          routeBasePath: '/',
          sidebarPath: require.resolve('./sidebars.js'),
          remarkPlugins: [math],
          rehypePlugins: [katex],
          editUrl:
            'https://github.com/xiaoyang-sde/programming-note/edit/master/',
        },
        blog: false,
        theme: {
          customCss: require.resolve('./src/css/custom.css'),
        },
        gtag: {
          trackingID: 'G-HXM4HDSQBY',
          anonymizeIP: true,
        },
      },
    ],
  ],
};
