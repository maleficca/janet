import React from 'react';
import clsx from 'clsx';
import styles from './styles.module.css';

const FeatureList = [
  {
    title: 'Simple automation',
    Svg: require('@site/static/img/undraw_animating_re_5gvn.svg').default,
    description: (
      <>
        Janet is a small tool which compiles and runs C# scripts in Revit,
        just by pressing hotkeys.
      </>
    ),
  },
  {
    title: 'Change what you need',
    Svg: require('@site/static/img/undraw_software_engineer_re_tnjc.svg').default,
    description: (
      <>
        Editing scripts, called Blocks is just as simple as editing text files.
        You can use your favourite text editor to do it!
      </>
    ),
  },
  {
    title: 'The power of community',
    Svg: require('@site/static/img/undraw_people_re_8spw.svg').default,
    description: (
      <>
        Janet is backed by Revit experts and power-users, eager to help with
        your automation needs and navigation in the boundless void of Revit.
      </>
    ),
  },
];

function Feature({Svg, title, description}) {
  return (
    <div className={clsx('col col--4')}>
      <div className="text--center">
        <Svg className={styles.featureSvg} role="img" />
      </div>
      <div className="text--center padding-horiz--md">
        <h3>{title}</h3>
        <p>{description}</p>
      </div>
    </div>
  );
}

export default function HomepageFeatures() {
  return (
    <section className={styles.features}>
      <div className="container">
        <div className="row">
          {FeatureList.map((props, idx) => (
            <Feature key={idx} {...props} />
          ))}
        </div>
      </div>
    </section>
  );
}
