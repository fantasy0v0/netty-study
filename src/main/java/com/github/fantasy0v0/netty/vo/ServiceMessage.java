package com.github.fantasy0v0.netty.vo;

import io.netty.buffer.ByteBuf;

/**
 * 服务消息
 */
public class ServiceMessage {

  /**
   * 消息类型 0 请求 1 响应
   */
  private byte type;

  /**
   * 消息编号
   */
  private short id;

  /**
   * 业务类型
   */
  private short businessType;

  /**
   * 业务数据
   */
  private ByteBuf data;

  public byte getType() {
    return type;
  }

  public void setType(byte type) {
    this.type = type;
  }

  public short getId() {
    return id;
  }

  public void setId(short id) {
    this.id = id;
  }

  public short getBusinessType() {
    return businessType;
  }

  public void setBusinessType(short businessType) {
    this.businessType = businessType;
  }

  public ByteBuf getData() {
    return data;
  }

  public void setData(ByteBuf data) {
    this.data = data;
  }
}
