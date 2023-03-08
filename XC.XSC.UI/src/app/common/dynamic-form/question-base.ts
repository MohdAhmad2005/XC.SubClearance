

export class Validators {
  id:number;
  name: string;
  validator: any;
  message: string;
}

export class QuestionBase<T> {
  dropDownUri: string;
  class:string;
  value: T;
  key: string;
  label: string;
  required: boolean;
  order: number;
  controlType: string;
  active:boolean;
  maxLength: number;
  type: string;
  validations: Validators[] = [];
  options: { key: string, value: string }[];
  processVariable: boolean;
  readOnly: boolean;
  showIcon: boolean;
  toggleOn?:string;
  toggleOff?:string;
  maxlength?:number;
  placeholder?:string;
  constructor(options: {
    value?: T,
    key?: string,
    label?: string,
    required?: boolean,
    order?: number,
    controlType?: string,
    type?: string,
    processVariable?: boolean,
    dropDownUri?: string,
    validations?: Validators[]
    readonly?: boolean,
    active?:boolean,
    toggleOn?:string,
    toggleOff?:string,
    maxlength?:number,
    placeholder?:string;
  } = {}) {
    this.value = options.value;
    this.key = options.key || '';
    this.label = options.label || '';
    this.required = !!options.required;
    this.order = options.order === undefined ? 1 : options.order;
    this.controlType = options.controlType || '';
    this.type = options.type || '';
    this.validations = options.validations;
    this.processVariable = options.processVariable;
    this.dropDownUri = options.dropDownUri;
    this.readOnly = !!options.readonly;
    this.active = options.active;
    this.placeholder = options.placeholder || '';
  }
}
