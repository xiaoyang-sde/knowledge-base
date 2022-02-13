import React from 'react';
import clsx from 'clsx';
import Layout from '@theme/Layout';
import Link from '@docusaurus/Link';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import styles from './index.module.css';

function Banner(): JSX.Element {
  const { siteConfig } = useDocusaurusContext();

  return (
    <header className={clsx('hero', styles.heroBanner)}>
      <div className={clsx('container', styles.flexContainer)}>
        <div>
          <h1 className="hero__title">{siteConfig.title}</h1>

          <p className="hero__subtitle">
            Lecture notes on Computer Science,
            <br />
            Web Development, and Software Engineering Philosophies.
            <br />
            (Berkeley CS 61B, CMU 15-213, MIT 6.824, etc.)
          </p>

          <div className={styles.buttons}>
            <Link
              className={clsx('button button--primary button--lg', styles.heroButton)}
              to="/docs/intro"
            >
              Introduction
            </Link>

            <Link
              className={clsx('button button--secondary button--lg', styles.heroButton)}
              to="https://github.com/xiaoyang-sde/programming-note"
            >
              GitHub
            </Link>
          </div>
        </div>
      </div>
    </header>
  );
}

function Home(): JSX.Element {
  return (
    <Layout
      title="Home"
      description="Description will go into a meta tag in <head />"
    >
      <Banner />
    </Layout>
  );
}

export default Home;
