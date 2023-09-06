import React from 'react';
import ComponentCreator from '@docusaurus/ComponentCreator';

export default [
  {
    path: '/blog/',
    component: ComponentCreator('/blog/', 'd65'),
    exact: true
  },
  {
    path: '/blog/archive/',
    component: ComponentCreator('/blog/archive/', '846'),
    exact: true
  },
  {
    path: '/blog/first-blog-post/',
    component: ComponentCreator('/blog/first-blog-post/', '3e9'),
    exact: true
  },
  {
    path: '/blog/long-blog-post/',
    component: ComponentCreator('/blog/long-blog-post/', '870'),
    exact: true
  },
  {
    path: '/blog/mdx-blog-post/',
    component: ComponentCreator('/blog/mdx-blog-post/', '494'),
    exact: true
  },
  {
    path: '/blog/tags/',
    component: ComponentCreator('/blog/tags/', '353'),
    exact: true
  },
  {
    path: '/blog/tags/docusaurus/',
    component: ComponentCreator('/blog/tags/docusaurus/', '03d'),
    exact: true
  },
  {
    path: '/blog/tags/facebook/',
    component: ComponentCreator('/blog/tags/facebook/', '8ac'),
    exact: true
  },
  {
    path: '/blog/tags/hello/',
    component: ComponentCreator('/blog/tags/hello/', '45b'),
    exact: true
  },
  {
    path: '/blog/tags/hola/',
    component: ComponentCreator('/blog/tags/hola/', '701'),
    exact: true
  },
  {
    path: '/blog/welcome/',
    component: ComponentCreator('/blog/welcome/', 'cdc'),
    exact: true
  },
  {
    path: '/markdown-page/',
    component: ComponentCreator('/markdown-page/', 'e57'),
    exact: true
  },
  {
    path: '/docs/',
    component: ComponentCreator('/docs/', 'a91'),
    routes: [
      {
        path: '/docs/block-library/approve/',
        component: ComponentCreator('/docs/block-library/approve/', 'f31'),
        exact: true,
        sidebar: "tutorialSidebar"
      },
      {
        path: '/docs/block-library/change-params-to-type/',
        component: ComponentCreator('/docs/block-library/change-params-to-type/', 'e9b'),
        exact: true,
        sidebar: "tutorialSidebar"
      },
      {
        path: '/docs/block-library/create_families/',
        component: ComponentCreator('/docs/block-library/create_families/', '65e'),
        exact: true,
        sidebar: "tutorialSidebar"
      },
      {
        path: '/docs/block-library/model_lintels/',
        component: ComponentCreator('/docs/block-library/model_lintels/', '125'),
        exact: true,
        sidebar: "tutorialSidebar"
      },
      {
        path: '/docs/block-library/reject/',
        component: ComponentCreator('/docs/block-library/reject/', 'b43'),
        exact: true,
        sidebar: "tutorialSidebar"
      },
      {
        path: '/docs/block-library/test/',
        component: ComponentCreator('/docs/block-library/test/', 'c87'),
        exact: true,
        sidebar: "tutorialSidebar"
      },
      {
        path: '/docs/category/block-library/',
        component: ComponentCreator('/docs/category/block-library/', '231'),
        exact: true,
        sidebar: "tutorialSidebar"
      },
      {
        path: '/docs/category/tutorial---basics/',
        component: ComponentCreator('/docs/category/tutorial---basics/', '843'),
        exact: true,
        sidebar: "tutorialSidebar"
      },
      {
        path: '/docs/download/',
        component: ComponentCreator('/docs/download/', '00a'),
        exact: true,
        sidebar: "tutorialSidebar"
      },
      {
        path: '/docs/tutorial-basics/adding-new-blocks/',
        component: ComponentCreator('/docs/tutorial-basics/adding-new-blocks/', '8bc'),
        exact: true,
        sidebar: "tutorialSidebar"
      },
      {
        path: '/docs/tutorial-basics/adding-shortcut/',
        component: ComponentCreator('/docs/tutorial-basics/adding-shortcut/', 'eab'),
        exact: true,
        sidebar: "tutorialSidebar"
      },
      {
        path: '/docs/tutorial-basics/congratulations/',
        component: ComponentCreator('/docs/tutorial-basics/congratulations/', 'cb6'),
        exact: true,
        sidebar: "tutorialSidebar"
      },
      {
        path: '/docs/tutorial-basics/starting-janet/',
        component: ComponentCreator('/docs/tutorial-basics/starting-janet/', '43f'),
        exact: true,
        sidebar: "tutorialSidebar"
      },
      {
        path: '/docs/tutorial-basics/working-with-blocks/',
        component: ComponentCreator('/docs/tutorial-basics/working-with-blocks/', '445'),
        exact: true,
        sidebar: "tutorialSidebar"
      }
    ]
  },
  {
    path: '/',
    component: ComponentCreator('/', 'af5'),
    exact: true
  },
  {
    path: '*',
    component: ComponentCreator('*'),
  },
];
