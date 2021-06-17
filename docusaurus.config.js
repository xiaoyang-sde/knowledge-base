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
  projectName: 'programming-notes',
  stylesheets: [
    'https://fonts.googleapis.com/css2?family=Open+Sans&display=swap',
    'https://cdn.jsdelivr.net/npm/katex@0.12.0/dist/katex.min.css',
  ],
  themeConfig: {
    googleAnalytics: {
      trackingID: 'UA-156941792-1',
      anonymizeIP: true,
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
          to: '/blog',
          label: 'Blog',
          position: 'left',
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
      links: [
        {
          title: 'Computer Science',
          items: [
            {
              label: 'Algorithm Design',
              to: 'docs/computer-science/algorithm-design/stable-matching',
            },
            {
              label: 'Computer Systems',
              to: 'docs/computer-science/computer-systems/bits-bytes-integers',
            },
            {
              label: 'Data Structures',
              to: 'docs/computer-science/data-structures/hello-world-java',
            },
          ],
        },
        {
          title: 'Web Development',
          items: [
            {
              label: 'The Modern JavaScript Tutorial',
              to: 'docs/web-development/javascript-tutorial/the-javascript-language/an-introduction',
            },
            {
              label: 'You Don\'t Know JS',
              to: 'docs/web-development/you-dont-know-js/get-started/what-is-javascript',
            },
            {
              label: 'TypeScript Handbook',
              to: 'docs/web-development/typescript-handbook/basic-types',
            },
          ],
        },
        {
          title: 'Software Engineering',
          items: [
            {
              label: 'Agile Development',
              to: 'docs/software-engineering/agile-development/agile-manifesto',
            },
            {
              label: 'System Design',
              to: 'docs/software-engineering/system-design/aws-csa-notes-2019/applications',
            },
          ],
        },
      ],
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
            'https://github.com/xiaoyang-sde/programming-notes/edit/master/',
        },
        blog: {
          showReadingTime: true,
          // Please change this to your repo.
          editUrl:
            'https://github.com/facebook/docusaurus/edit/master/blog/',
        },
        theme: {
          customCss: require.resolve('./src/css/custom.css'),
        },
      },
    ],
  ],
  plugins: ['@docusaurus/plugin-google-analytics'],
};
