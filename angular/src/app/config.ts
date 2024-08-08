export const Config = {
    get: () => ({
      prefix: 'your-prefix'
    })
  };

  export class FRLogger {
    static warn(message: string): void {
      console.warn(message);
    }
  };