import fs from 'fs/promises';
import path from 'path';
import sharp from 'sharp';

const inDir = 'src/assets/prewedding-originals';
const outDir = 'public/gallery';

const widths = [400, 800, 1200, 2000];
const qualityJpg = 80;
const qualityWebp = 75;
const qualityAvif = 45;

await fs.mkdir(outDir, { recursive: true });

const files = (await fs.readdir(inDir)).filter((f) => /\.(jpe?g|png|tiff)$/i.test(f));

for (const file of files) {
  const base = path.parse(file).name.replace(/\s+/g, '-').replace(/-+/g, '-').toLowerCase();

  for (const w of widths) {
    const img = sharp(path.join(inDir, file))
      .rotate()
      .resize({ width: w, withoutEnlargement: true });

    await img
      .jpeg({ quality: qualityJpg, mozjpeg: true })
      .toFile(path.join(outDir, `${base}-${w}.jpg`));

    await img.webp({ quality: qualityWebp }).toFile(path.join(outDir, `${base}-${w}.webp`));

    await img.avif({ quality: qualityAvif }).toFile(path.join(outDir, `${base}-${w}.avif`));
  }
}
console.log('Gallery built in', outDir);
